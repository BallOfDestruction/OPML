using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OPML.Core
{
    public class Outline
    {
        ///<summary>
        /// Text of the XML file (required)
        ///</summary>
        public string Text { get; set; }

        ///<summary>
        /// true / false
        ///</summary>
        public string IsComment { get; set; }

        ///<summary>
        /// true / false
        ///</summary>
        public string IsBreakpoint { get; set; }

        public DateTime? Created { get; set; }

        public List<string> Category { get; set; } = new List<string>();

        public string Description { get; set; }

        public string HtmlUrl { get; set; }

        public string Language { get; set; }

        public string Title { get; set; }

        ///<summary>
        /// Type (rss/atom)
        ///</summary>
        public string Type { get; set; }

        ///<summary>
        /// Version of RSS. 
        /// RSS1 for RSS1.0. RSS for 0.91, 0.92 or 2.0.
        ///</summary>
        public string Version { get; set; }

        public string XmlUrl { get; set; }

        public List<Outline> Outlines { get; set; } = new List<Outline>();

        public Outline()
        {

        }

        public Outline(XmlElement element)
        {
            Text = element.GetAttribute("text");
            IsComment = element.GetAttribute("isComment");
            IsBreakpoint = element.GetAttribute("isBreakpoint");
            Created = GetDateTimeAttribute(element, "created");
            Category = GetCategoriesAttribute(element, "category");
            Description = element.GetAttribute("description");
            HtmlUrl = element.GetAttribute("htmlUrl");
            Language = element.GetAttribute("language");
            Title = element.GetAttribute("title");
            Type = element.GetAttribute("type");
            Version = element.GetAttribute("version");
            XmlUrl = element.GetAttribute("xmlUrl");

            if (element.HasChildNodes)
            {
                foreach (XmlNode child in element.ChildNodes)
                {
                    if (child.Name.Equals("outline", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Outlines.Add(new Outline((XmlElement) child));
                    }
                }
            }
        }

        private DateTime? GetDateTimeAttribute(XmlElement element, string name)
        {
            var dt = element.GetAttribute(name);

            try
            {
                return DateTime.Parse(dt);
            }
            catch
            {
                return null;
            }
        }

        private List<string> GetCategoriesAttribute(XmlElement element, string name)
        {
            return element.GetAttribute(name).Split(',').Select(item => item.Trim()).ToList();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<outline");
            builder.Append(GetAttributeString("text", Text));
            builder.Append(GetAttributeString("isComment", IsComment));
            builder.Append(GetAttributeString("isBreakpoint", IsBreakpoint));
            builder.Append(GetAttributeString("created", Created));
            builder.Append(GetAttributeString("category", Category));
            builder.Append(GetAttributeString("description", Description));
            builder.Append(GetAttributeString("htmlUrl", HtmlUrl));
            builder.Append(GetAttributeString("language", Language));
            builder.Append(GetAttributeString("title", Title));
            builder.Append(GetAttributeString("type", Type));
            builder.Append(GetAttributeString("version", Version));
            builder.Append(GetAttributeString("xmlUrl", XmlUrl));

            if (Outlines.Count > 0)
            {
                builder.Append(">\r\n");
                foreach (var outline in Outlines)
                {
                    builder.Append(outline);
                }

                builder.Append("</outline>\r\n");
            }
            else
            {
                builder.Append(" />\r\n");
            }

            return builder.ToString();
        }

        private string GetAttributeString(string name, string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : $" {name}=\"{value}\"";
        }

        private string GetAttributeString(string name, DateTime? value)
        {
            return value == null ? string.Empty : $" {name}=\"{value?.ToString("R")}\"";
        }

        private string GetAttributeString(string name, IReadOnlyCollection<string> value)
        {
            if (value.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var item in value)
            {
                builder.Append(item);
                builder.Append(",");
            }

            return $" {name}=\"{builder.Remove(builder.Length - 1, 1)}\"";
        }
    }
}