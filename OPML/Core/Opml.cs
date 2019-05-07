using System;
using System.Text;
using System.Xml;

namespace OPML.Core
{
    public class Opml
    {
        public string Version { get; set; }

        public string Encoding { get; set; }

        public Head Head { get; set; } = new Head();

        public Body Body { get; set; } = new Body();

        public Opml()
        {

        }

        public Opml(string location)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(location);

            ReadOpmlNodes(xmlDocument);
        }

        public Opml(XmlDocument doc)
        {
            ReadOpmlNodes(doc);
        }

        private void ReadOpmlNodes(XmlDocument document)
        {
            foreach (XmlNode nodes in document)
            {
                if (!nodes.Name.Equals("opml", StringComparison.CurrentCultureIgnoreCase)) continue;

                foreach (XmlNode childNode in nodes)
                {
                    if (childNode.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Head = new Head((XmlElement) childNode);
                    }

                    if (childNode.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Body = new Body((XmlElement) childNode);
                    }
                }
            }
        }

        public override string ToString()
        {
            var encoding = string.IsNullOrEmpty(Encoding) ? "UTF-8" : Encoding;
            var version = string.IsNullOrEmpty(Version) ? "2.0" : Version;
            
            var builder = new StringBuilder();
            builder.Append($"<?xml version=\"1.0\" encoding=\"{encoding}\" ?>\r\n");
            builder.Append($"<opml version=\"{version}\">\r\n");
            builder.Append(Head);
            builder.Append(Body);
            builder.Append("</opml>");

            return builder.ToString();
        }
    }
}