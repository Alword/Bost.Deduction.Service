using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Model
{
	public class AgentState
	{
		public string AgentName { get; set; }
		public string StateName { get; set; }
		public bool IsActivation { get; set; }
		public AgentState(string agentName, string stateName, bool isActiovation = true)
		{
			AgentName = agentName;
			StateName = stateName;
			IsActivation = isActiovation;
		}
	}
}
