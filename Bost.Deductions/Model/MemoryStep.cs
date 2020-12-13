using Bost.Deductions.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Model
{
	public class MemoryStep
	{
		public string ActionName { get; }
		public string[] StatusChange { get; }
		public Rectangle Action { get; }
		public Cube[] ActionAgents { get; }
		public MemoryStep? Parent { get; }
		public MemoryStep(Rectangle action, MemoryStep? parent = null)
		{
			Action = action;
			ActionName = action.Value;
			Parent = parent;
			StatusChange = action.Outgoing.Where(e => e is Ellipse).Select(e => e.Value).ToArray();
			ActionAgents = action.Incoming.Where(e => e is Cube).Select(e => (Cube)e).ToArray();
		}

		/// <summary>
		/// Additional steps for current action
		/// </summary>
		/// <returns></returns>
		public AgentState[] ExtractAdditionalSteps()
		{
			List<AgentState> additionalSteps = new List<AgentState>();
			foreach (var agent in ActionAgents)
			{
				var states = from state in agent.Incoming.Where(e => e is Ellipse)
							 where state.Incoming.Count == 0
							 select (Ellipse)state;

				foreach (var state in states)
				{
					AgentState agentState = new(agent.Value, state.Value, state.IsActivation);
					additionalSteps.Add(agentState);
				}
			}
			return additionalSteps.ToArray();
		}

		public override bool Equals(object? obj)
		{
			return obj is MemoryStep step && EqualityComparer<Rectangle>.Default.Equals(Action, step.Action);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Action);
		}
	}
}
