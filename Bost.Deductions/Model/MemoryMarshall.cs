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

		public List<string> SetTargetState(string agent, string targetState, bool activation = true)
		{
			targetState = targetState.ToLower();
			agent = agent.ToLower();

			//Stack<>

			var targetStatesNodes = _memoryNetwork.States[targetState];

			var targetPoints = from node in targetStatesNodes.Where(e => e.IsActivation == activation)
							   where node.Incoming.Any(e => e is Rectangle)
							   where node.Outgoing.Any(e => e is Cube cube && cube.Value == agent)
							   select node;
			var targetArray = targetPoints.ToArray();

			throw new NotImplementedException();
		}
	}
}
