using Bost.Deductions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bost.Deductions.Model
{
	public abstract class Shape
	{
		public string Id { get; set; }
		public string Value { get; set; }
		public dynamic? Context { get; set; }
		public List<Shape> Incoming { get; set; }
		public List<Shape> Outgoing { get; set; }
		public abstract Shape? VlidateShape(XmlNode xmlNode);
		public virtual void AddToNetwork(MemoryNetwork network)
		{
			if (Id == string.Empty) return;
			network.Nodes.Add(Id, this);
		}
		public Shape()
		{
			Id = string.Empty;
			Value = string.Empty;
			Incoming = new List<Shape>();
			Outgoing = new List<Shape>();
		}
		public Shape(string id, string? value)
		{
			Value = value ?? string.Empty;
			Id = id;
			Incoming = new List<Shape>();
			Outgoing = new List<Shape>();
		}

		public Shape(XmlNode? xmlNode)
		{
			if (xmlNode == null || xmlNode.Attributes == null)
			{
				Value = string.Empty;
				Id = string.Empty;
				Incoming = new List<Shape>();
				Outgoing = new List<Shape>();
			}
			else
			{
				Id = xmlNode.Attributes["id"]?.Value ?? string.Empty;
				Value = xmlNode.Attributes["value"]?.Value.ToLower() ?? string.Empty;
				Incoming = new List<Shape>();
				Outgoing = new List<Shape>();
			}
		}

		public override string ToString()
		{
			return Value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Shape shape && Id == shape.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}
	}
}
