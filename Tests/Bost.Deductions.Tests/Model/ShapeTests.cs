using Bost.Deductions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bost.Deductions.Tests.Model
{
	public class ShapeTests
	{
		[Fact]
		public static void LoadShapes()
		{
			var types = Shape.LoadShapes();
		}
	}
}
