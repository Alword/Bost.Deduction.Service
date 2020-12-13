using Bost.Deductions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bost.Deductions.Extentions
{
	public static class ShapeExtentions
	{
		public static Shape[] LoadShapes()
		{
			var shapes = typeof(Shape).Assembly.GetTypes().Where(e => e.BaseType == typeof(Shape)).ToArray();
			var shapesArray = shapes.Select(e => Activator.CreateInstance(e) as Shape).ToArray();
#pragma warning disable CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
			return shapesArray ?? Array.Empty<Shape>(); ;
#pragma warning restore CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
		}
	}
}
