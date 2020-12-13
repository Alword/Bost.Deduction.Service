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
	public class ShapeNetwork
	{
		public static readonly Shape[] Shapes = ShapeExtentions.LoadShapes();

		/// <summary>Relations by name</summary>
		public List<Arrow> TempArrows { get; set; }

		/// <summary>Actions by name</summary>
		public Dictionary<string, HashSet<Rectangle>> Rectangles { get; set; }

		/// <summary>Actors by name</summary>
		public Dictionary<string, HashSet<Cube>> Cubes { get; set; }

		/// <summary>State by name</summary>
		public Dictionary<string, HashSet<Ellipse>> Ellipses { get; set; }

		/// <summary>Id to Node</summary>
		public Dictionary<string, Shape> Nodes { get; set; }

		public ShapeNetwork()
		{
			TempArrows = new List<Arrow>();
			Nodes = new Dictionary<string, Shape>();
			Cubes = new Dictionary<string, HashSet<Cube>>();
			Ellipses = new Dictionary<string, HashSet<Ellipse>>();
			Rectangles = new Dictionary<string, HashSet<Rectangle>>();
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
				TempArrows.Clear();
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
			foreach (var arrow in TempArrows)
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

		public Shape? RecognizeShape(XmlNode shapeNode)
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
