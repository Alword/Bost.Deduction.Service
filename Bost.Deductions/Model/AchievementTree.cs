using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Model
{
	public class AchievementTree : IEnumerator<MemoryStep?>
	{
		public Queue<MemoryStep> MemorySteps { get; }
		public HashSet<MemoryStep> Shapes { get; }

		public AchievementTree(MemoryStep[] memorySteps)
		{
			MemorySteps = new Queue<MemoryStep>(memorySteps.Length);
			Shapes = new HashSet<MemoryStep>(memorySteps.Length);
			foreach (var step in memorySteps)
			{
				MemorySteps.Enqueue(step);
				Shapes.Add(step);
			}
		}
		public AchievementTree()
		{
			MemorySteps = new Queue<MemoryStep>();
			Shapes = new HashSet<MemoryStep>();
		}

		public void Update(params MemoryStep[] memorySteps)
		{
			foreach (var step in memorySteps)
			{
				if (Shapes.Contains(step)) continue;

				Shapes.Add(step);
				MemorySteps.Enqueue(step);
			}
		}



		private MemoryStep? _current;
		public MemoryStep? Current => _current;
		object IEnumerator.Current => this;
		public MemoryStep NextStep() => MemorySteps.Dequeue();

		public IEnumerator<MemoryStep?> GetEnumerator() => this;

		public bool MoveNext()
		{
			var hasNext = MemorySteps.Any();

			if (hasNext)
				_current = MemorySteps.Dequeue();
			return hasNext;
		}

		public void Reset() { throw new NotImplementedException(); }

		public void Dispose()
		{
			MemorySteps.Clear();
			Shapes.Clear();
		}
	}
}
