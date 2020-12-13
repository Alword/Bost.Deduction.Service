using Bost.Deductions.Model;
using System;
using Xunit;

namespace Bost.Deductions.Tests
{
	public class DiagramTest
	{
		[Fact]
		public void DigramDownload_ShouldBeFine()
		{
			SemanticFile semantic = new();
			var memoryNetwork = semantic.BuildGraph();

			MemoryMarshall memoryMarshall = new MemoryMarshall(memoryNetwork);
			var test = memoryMarshall.SetTargetState("bot", "Авторизован");
		}
	}
}
