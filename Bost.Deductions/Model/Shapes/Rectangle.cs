﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bost.Deductions.Model.Shapes
{
	public class Rectangle : Shape
	{
		public Rectangle() { }
		public Rectangle(XmlNode? node) : base(node) { }
		public override Shape? VlidateShape(XmlNode xmlNode)
		{
			return new Rectangle(xmlNode);
		}
		public override void AddToNetwork(MemoryNetwork network)
		{
			if (Value == string.Empty) return;

			HashSet<Rectangle>? cubes;

			if (!network.Actions.ContainsKey(Value))
			{
				network.Actions.Add(Value, new HashSet<Rectangle>());
			}

			cubes = network.Actions[Value];
			if (cubes.Contains(this)) return;
			cubes.Add(this);
			base.AddToNetwork(network);
		}
	}
}
