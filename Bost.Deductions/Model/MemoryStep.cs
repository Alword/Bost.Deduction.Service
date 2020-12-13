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
		public Cube[] BackPropagation { get; }
		public MemoryStep(Rectangle action)
		{
			Action = action;
			ActionName = action.Value;
			StatusChange = action.Outgoing.Where(e => e is Ellipse).Select(e => e.Value).ToArray();
			BackPropagation = action.Incoming.Where(e => e is Cube).Select(e => (Cube)e).ToArray();
		}
	}
}
