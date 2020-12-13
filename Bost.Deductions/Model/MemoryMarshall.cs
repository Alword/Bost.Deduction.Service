using Bost.Deductions.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Model
{
	public class MemoryMarshall
	{
		private readonly MemoryNetwork _memoryNetwork;

		public MemoryMarshall(MemoryNetwork memoryNetwork)
		{
			_memoryNetwork = memoryNetwork;
		}

		public MemoryStep? SetTargetState(string agent, string targetState, bool activation = true)
		{
			targetState = targetState.ToLower();
			agent = agent.ToLower();

			var actions = TargetState(agent, targetState, activation);

			AchievementTree memorySteps = new AchievementTree();
			
			foreach (var action in actions)
			{
				if (action == null) continue;
				MemoryStep memoryStep = new(action, null);
				memorySteps.Update(memoryStep);
			}

			foreach (var memoryStep in memorySteps)
			{
				if (memoryStep == null) continue;

				AgentState[]? additionalStates = memoryStep.ExtractAdditionalSteps();

				var stepsToTarget = CanExecute(additionalStates);

				if (!stepsToTarget.Any()) return memoryStep;

				foreach (var step in stepsToTarget)
				{
					var extraMemorySteps = TargetState(step);
					foreach (var action in extraMemorySteps)
					{
						if (action == null) continue;
						MemoryStep additionalStep = new(action, memoryStep);
						memorySteps.Update(additionalStep);
					}
				}

			}
			return null;
		}

		private Rectangle?[] TargetState(string agent, string targetState, bool activation)
		{
			var targetStatesNodes = _memoryNetwork.States[targetState];

			var targetPoints = from node in targetStatesNodes.Where(e => e.IsActivation == activation)
							   where node.Incoming.Any(e => e is Rectangle)
							   where node.Outgoing.Any(e => e is Cube cube && cube.Value == agent)
							   select node.Incoming.Where(e => e is Rectangle).Select(e => (Rectangle)e);
			return targetPoints.SelectMany(e => e).ToArray();
		}
		private Rectangle?[] TargetState(AgentState state)
		{
			return TargetState(state.AgentName, state.StateName, state.IsActivation);
		}

		private AgentState[] CanExecute(AgentState[]? additionalSteps)
		{
			List<AgentState> stepsToComplete = new List<AgentState>();

			if (additionalSteps is null || !additionalSteps.Any()) return Array.Empty<AgentState>();

			foreach (var step in additionalSteps)
			{
				var (context, _) = _memoryNetwork.Agents[step.AgentName];
				bool canExecute = (context.HasState(step.StateName) > 0) == step.IsActivation;
				if (!canExecute) stepsToComplete.Add(step);
			}
			return stepsToComplete.ToArray();
		}
	}
}
