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
			var graph = semantic.BuildGraph();
		}
	}
}
