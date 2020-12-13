using Bost.Deductions.Model;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace Bost.Deductions
{
	public class SemanticFile
	{
		private static readonly string _path = "https://drive.google.com/u/0/uc?id=1dZbzbNHOM5lDBf0AvkC83qm8fp8C8_jq&export=download";
		public MemoryNetwork BuildGraph(string pageMask = ".*")
		{
			Regex regex = new Regex(pageMask);
			using var xml = new WebClient();
			var drawio = xml.DownloadString(_path);

			MemoryNetwork shapeNetwork = new();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(drawio);
			if (xmlDocument.DocumentElement == null) return shapeNetwork;

			XmlNodeList? pages = xmlDocument.DocumentElement.SelectNodes("/mxfile/diagram");

			if (pages == null) return shapeNetwork;

			foreach (XmlNode pageXml in pages)
			{
				if (pageXml.Attributes == null) continue;

				var name = pageXml.Attributes["name"]?.Value ?? string.Empty;

				if (!regex.IsMatch(name)) continue;

				var page = DiagramsDecompressor.Decompress(pageXml.InnerXml);
				shapeNetwork.AppendNetwork(page);
			}

			return shapeNetwork;
		}
	}
}
