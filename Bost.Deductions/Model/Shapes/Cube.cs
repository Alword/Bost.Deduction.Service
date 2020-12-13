using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bost.Deductions.Model.Shapes
{
	public class Cube : Shape
	{
		public Cube() { }
		public Cube(XmlNode? value) : base(value) { }
		public override Shape? VlidateShape(XmlNode xmlNode)
		{
			if (xmlNode.Attributes == null) return null;

			var style = xmlNode.Attributes["style"]?.Value;

			if (style == null || !style.StartsWith("shape=cube")) return null;

			return new Cube(xmlNode);
		}

		public override void AddToNetwork(ShapeNetwork network)
		{
			if (Value == string.Empty) return;

			if (!network.Cubes.ContainsKey(Value))
			{
				network.Cubes.Add(Value, new HashSet<Cube>());
			}
			var cubes = network.Cubes[Value];
			
			if (cubes.Contains(this)) return;

			cubes.Add(this);
			base.AddToNetwork(network);
		}
	}
}
