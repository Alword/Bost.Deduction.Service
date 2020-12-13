using Bost.Deductions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bost.Deductions.Model.Shapes
{
	public class Ellipse : Shape
	{
		private const int _colorLength = 7;
		private const string _colorAttribute = "fillColor=";
		private static readonly int _colorAttributeLength = _colorAttribute.Length;

		public Colors Color { get; set; }
		public bool IsActivation => Color == Colors.Green;
		public Ellipse() { }
		public Ellipse(Colors color, string id, string? value) : base(id, value)
		{
			Color = color;
		}

		public Ellipse(Colors color, XmlNode? value) : base(value)
		{
			Color = color;
		}

		public override Shape? VlidateShape(XmlNode xmlNode)
		{
			if (xmlNode.Attributes == null) return null;

			var style = xmlNode.Attributes["style"]?.Value;

			if (style == null || !style.StartsWith("ellipse")) return null;

			var colorString = string.Empty;
			var colorStringIndex = style.IndexOf("fillColor=");

			if (colorStringIndex > -1)
			{
				var start = colorStringIndex + _colorAttributeLength;
				var end = start + _colorLength;
				colorString = style[start..end];
			}

			return new Ellipse(ColorsSwitch.FromRgb(colorString), xmlNode);
		}

		public override void AddToNetwork(MemoryNetwork network)
		{
			if (Value == string.Empty) return;

			if (!network.States.ContainsKey(Value))
			{
				network.States.Add(Value, new HashSet<Ellipse>());
			}

			var cubes = network.States[Value];
			if (cubes.Contains(this)) return;
			cubes.Add(this);
			base.AddToNetwork(network);
		}
	}
}
