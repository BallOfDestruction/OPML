using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OPML.Core
{
    public class Head
    {
        public string Title { get; set; }

        public DateTime? DateCreated { get; set; }
        
        public DateTime? DateModified { get; set; }

        public string OwnerName { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerId { get; set; }

        public string Docs { get; set; }

        public List<string> ExpansionState { get; set; } = new List<string>();

        public string VertScrollState { get; set; }

        public string WindowTop { get; set; }

        public string WindowLeft { get; set; }

        public string WindowBottom { get; set; }

        public string WindowRight { get; set; }

        public Head()
        {

        }

        public Head(XmlElement element)
        {
            if (element.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    Title = GetStringValue(node, "title", Title);
                    DateCreated = GetDateTimeValue(node, "dateCreated", DateCreated);
                    DateModified = GetDateTimeValue(node, "dateModified", DateModified);
                    OwnerName = GetStringValue(node, "ownerName", OwnerName);
                    OwnerEmail = GetStringValue(node, "ownerEmail", OwnerEmail);
                    OwnerId = GetStringValue(node, "ownerId", OwnerId);
                    Docs = GetStringValue(node, "docs", Docs);
                    ExpansionState = GetExpansionState(node, "expansionState", ExpansionState);
                    VertScrollState = GetStringValue(node, "vertScrollState", VertScrollState);
                    WindowTop = GetStringValue(node, "windowTop", WindowTop);
                    WindowLeft = GetStringValue(node, "windowLeft", WindowLeft);
                    WindowBottom = GetStringValue(node, "windowBottom", WindowBottom);
                    WindowRight = GetStringValue(node, "windowRight", WindowRight);
                }
            }
        }

        private string GetStringValue(XmlNode node, string name, string value)
        {
            if (node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                return node.InnerText;
            }

            return !node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) ? value : string.Empty;
        }

        private DateTime? GetDateTimeValue(XmlNode node, string name, DateTime? value)
        {
            if (node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    return DateTime.Parse(node.InnerText);
                }
                catch
                {
                    return null;
                }
            }

            return !node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) ? value : null;
        }

        private List<string> GetExpansionState(XmlNode node, string name, List<string> value)
        {
            if (node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                return node.InnerText.Split(',').Select(item => item.Trim()).ToList();
            }

            return !node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) ? value : new List<string>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<head>\r\n");
            builder.Append(GetNodeString("title", Title));
            builder.Append(GetNodeString("dateCreated", DateCreated));
            builder.Append(GetNodeString("dateModified", DateModified));
            builder.Append(GetNodeString("ownerName", OwnerName));
            builder.Append(GetNodeString("ownerEmail", OwnerEmail));
            builder.Append(GetNodeString("ownerId", OwnerId));
            builder.Append(GetNodeString("docs", Docs));
            builder.Append(GetNodeString("expansionState", ExpansionState));
            builder.Append(GetNodeString("vertScrollState", VertScrollState));
            builder.Append(GetNodeString("windowTop", WindowTop));
            builder.Append(GetNodeString("windowLeft", WindowLeft));
            builder.Append(GetNodeString("windowBottom", WindowBottom));
            builder.Append(GetNodeString("windowRight", WindowRight));
            builder.Append("</head>\r\n");
            return builder.ToString();
        }

        private string GetNodeString(string name, string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : $"<{name}>{value}</{name}>\r\n";
        }

        private string GetNodeString(string name, DateTime? value)
        {
            return value == null ? string.Empty : $"<{name}>{value?.ToString("R")}</{name}>\r\n";
        }

        private string GetNodeString(string name, IReadOnlyCollection<string> value)
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

            return $"<{name}>{builder.Remove(builder.Length - 1, 1)}</{name}>\r\n";
        }
    }
}