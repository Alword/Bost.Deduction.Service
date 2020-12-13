using Bost.Deductions.Extentions;
using Bost.Deductions.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bost.Deductions.Model
{
	public class MemoryNetwork
	{
		public static readonly Shape[] Shapes = ShapeExtentions.LoadShapes();

		/// <summary>Relations by name</summary>
		public List<Arrow> TempRelations { get; set; }

		/// <summary>Actions by name</summary>
		public Dictionary<string, HashSet<Rectangle>> Actions { get; set; }

		/// <summary>Actors by name</summary>
		public Dictionary<string, (AgentContext, HashSet<Cube>)> Agents { get; set; }

		/// <summary>State by name</summary>
		public Dictionary<string, HashSet<Ellipse>> States { get; set; }

		/// <summary>Id to Node</summary>
		public Dictionary<string, Shape> Nodes { get; set; }

		public MemoryNetwork()
		{
			TempRelations = new List<Arrow>();
			Nodes = new Dictionary<string, Shape>();
			Agents = new Dictionary<string, (AgentContext, HashSet<Cube>)>();
			States = new Dictionary<string, HashSet<Ellipse>>();
			Actions = new Dictionary<string, HashSet<Rectangle>>();
		}

		public void AppendNetwork(string xmlPage)
		{
			lock (this)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xmlPage);

				if (doc.DocumentElement == null) return;

				XmlNodeList? shapes = doc.DocumentElement.SelectNodes("/mxGraphModel/root/mxCell");
				if (shapes == null) return;
				AppendShapes(shapes);
				LinkShapes();
				TempRelations.Clear();
			}
		}

		private void AppendShapes(XmlNodeList shapes)
		{
			foreach (XmlNode shapeNode in shapes)
			{
				if (shapeNode.Attributes == null) continue;

				var shape = RecognizeShape(shapeNode);

				if (shape == null) continue;

				shape.AddToNetwork(this);
			}
		}

		private void LinkShapes()
		{
			foreach (var arrow in TempRelations)
			{
				if (Nodes.ContainsKey(arrow.Source) && Nodes.ContainsKey(arrow.Target))
				{
					var sourse = Nodes[arrow.Source];
					var target = Nodes[arrow.Target];
					sourse.Outgoing.Add(target);
					target.Incoming.Add(sourse);
				}
			}
		}

		private Shape? RecognizeShape(XmlNode shapeNode)
		{
			foreach (var shape in Shapes)
			{
				var currentShape = shape.VlidateShape(shapeNode);
				if (currentShape != null)
				{
					return currentShape;
				}
			}
			return null;
		}
	}
}
