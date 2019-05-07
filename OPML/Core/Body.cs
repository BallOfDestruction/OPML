using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OPML.Core
{
    public class Body
    {
        public List<Outline> Outlines { get; } = new List<Outline>();

        public Body()
        {

        }

        public Body(XmlElement element)
        {
            if (element.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node.Name.Equals("outline", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Outlines.Add(new Outline((XmlElement) node));
                    }
                }
            }
        }

        public override string ToString()
        {
            var buf = new StringBuilder();
            buf.Append("<body>\r\n");
            
            foreach (var outline in Outlines)
            {
                buf.Append(outline);
            }

            buf.Append("</body>\r\n");

            return buf.ToString();
        }
    }
}