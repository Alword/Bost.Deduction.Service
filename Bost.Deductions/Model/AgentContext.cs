using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Model
{
	public class AgentContext
	{
		private readonly Dictionary<string, int> _state;
		public AgentContext()
		{
			_state = new Dictionary<string, int>();
		}
		public int HasState(string stateName)
		{
			var hasCount = _state.ContainsKey(stateName);
			return hasCount ? _state[stateName] : 0;
		}

		public void AddState(string state, int count = 0)
		{
			if (!_state.ContainsKey(state))
			{
				_state.Add(state, 0);
			}
		}
		public void RemoveState(string state, int count = 0)
		{
			if (_state.ContainsKey(state))
			{
				_state.Remove(state);
			}
		}
	}
}
