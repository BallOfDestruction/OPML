using System;
using System.Linq;
using System.Text;
using System.Xml;
using OPML.Core;
using Xunit;

namespace OPML.Test
{
    public class Tests
    {
        [Fact]
        public void NormalTest()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            xml.Append("<opml version=\"2.0\">");
            xml.Append("<head>");
            xml.Append("<title>mySubscriptions.opml</title>");
            xml.Append("<dateCreated>Sat, 18 Jun 2005 12:11:52 GMT</dateCreated>");
            xml.Append("<dateModified>Tue, 02 Aug 2005 21:42:48 GMT</dateModified>");
            xml.Append("<ownerName>fnya</ownerName>");
            xml.Append("<ownerEmail>fnya@example.com</ownerEmail>");
            xml.Append("<ownerId>http://news.com.com/</ownerId>");
            xml.Append("<docs>http://news.com.com/</docs>");
            xml.Append("<expansionState>1, 6, 13, 16, 18, 20</expansionState>");
            xml.Append("<vertScrollState>1</vertScrollState>");
            xml.Append("<windowTop>106</windowTop>");
            xml.Append("<windowLeft>106</windowLeft>");
            xml.Append("<windowBottom>558</windowBottom>");
            xml.Append("<windowRight>479</windowRight>");
            xml.Append("</head>");
            xml.Append("<body>");
            xml.Append("<outline ");
            xml.Append("text=\"CNET News.com\" ");
            xml.Append("isComment=\"true\" ");
            xml.Append("isBreakpoint=\"true\" ");
            xml.Append("created=\"Tue, 02 Aug 2005 21:42:48 GMT\" ");
            xml.Append("category=\"/Harvard/Berkman,/Politics\" ");
            xml.Append("description=\"Tech news and business reports by CNET News.com.\" ");
            xml.Append("htmlUrl=\"http://news.com.com/\" ");
            xml.Append("language=\"unknown\" ");
            xml.Append("title=\"CNET News.com\" ");
            xml.Append("type=\"rss\" ");
            xml.Append("version=\"RSS2\" ");
            xml.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" ");
            xml.Append("/>");
            xml.Append("</body>");
            xml.Append("</opml>");

            var doc = new XmlDocument();
            doc.LoadXml(xml.ToString());
            var opml = new Opml(doc);

            Assert.True(opml.Head.Title == "mySubscriptions.opml");
            Assert.True(opml.Head.DateCreated == DateTime.Parse("Sat, 18 Jun 2005 12:11:52 GMT"));
            Assert.True(opml.Head.DateModified == DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT"));
            Assert.True(opml.Head.OwnerName == "fnya");
            Assert.True(opml.Head.OwnerEmail == "fnya@example.com");
            Assert.True(opml.Head.OwnerId == "http://news.com.com/");
            Assert.True(opml.Head.Docs == "http://news.com.com/");
            Assert.True(opml.Head.ExpansionState.ToArray().SequenceEqual("1,6,13,16,18,20".Split(',')));
            Assert.True(opml.Head.VertScrollState == "1");
            Assert.True(opml.Head.WindowTop == "106");
            Assert.True(opml.Head.WindowLeft == "106");
            Assert.True(opml.Head.WindowBottom == "558");
            Assert.True(opml.Head.WindowRight == "479");

            foreach (var outline in opml.Body.Outlines)
            {
                Assert.True(outline.Text == "CNET News.com");
                Assert.True(outline.IsComment == "true");
                Assert.True(outline.IsBreakpoint == "true");
                Assert.True(outline.Created == DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT"));
                Assert.True(outline.Category.ToArray().SequenceEqual("/Harvard/Berkman,/Politics".Split(',')));
                Assert.True(outline.Description == "Tech news and business reports by CNET News.com.");
                Assert.True(outline.HtmlUrl == "http://news.com.com/");
                Assert.True(outline.Language == "unknown");
                Assert.True(outline.Title == "CNET News.com");
                Assert.True(outline.Type == "rss");
                Assert.True(outline.Version == "RSS2");
                Assert.True(outline.XmlUrl == "http://news.com.com/2547-1_3-0-5.xml");
            }
        }

        [Fact]
        public void ChildNodeTest()
        {
            var xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            xml.Append("<opml version=\"2.0\">");
            xml.Append("<head>");
            xml.Append("<title>mySubscriptions.opml</title>");
            xml.Append("</head>");
            xml.Append("<body>");
            xml.Append("<outline ");
            xml.Append("text=\"IT\" >");
            xml.Append("<outline ");
            xml.Append("text=\"washingtonpost.com\" ");
            xml.Append("htmlUrl=\"http://www.washingtonpost.com\" ");
            xml.Append("xmlUrl=\"http://www.washingtonpost.com/rss.xml\" ");
            xml.Append("/>");
            xml.Append("</outline>");
            xml.Append("</body>");
            xml.Append("</opml>");

            var doc = new XmlDocument();
            doc.LoadXml(xml.ToString());
            var opml = new Opml(doc);

            foreach (var outline in opml.Body.Outlines)
            {
                foreach (var childOutline in outline.Outlines)
                {
                    Assert.True(childOutline.Text == "washingtonpost.com");
                    Assert.True(childOutline.HtmlUrl == "http://www.washingtonpost.com");
                    Assert.True(childOutline.XmlUrl == "http://www.washingtonpost.com/rss.xml");
                }
            }
        }

        [Fact]
        public void CreateNormalOpmlTest()
        {
            var opml = new Opml();
            opml.Encoding = "UTF-8";
            opml.Version = "2.0";

            var head = new Head();
            head.Title = "mySubscriptions.opml";
            head.DateCreated = DateTime.Parse("Sat, 18 Jun 2005 12:11:52 GMT").ToUniversalTime();
            head.DateModified = DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT").ToUniversalTime();
            head.OwnerName = "fnya";
            head.OwnerEmail = "fnya@example.com";
            head.OwnerId = "http://news.com.com/";
            head.Docs = "http://news.com.com/";
            head.ExpansionState.Add("1");
            head.ExpansionState.Add("6");
            head.ExpansionState.Add("13");
            head.ExpansionState.Add("16");
            head.ExpansionState.Add("18");
            head.ExpansionState.Add("20");
            head.VertScrollState = "1";
            head.WindowTop = "106";
            head.WindowLeft = "106";
            head.WindowBottom = "558";
            head.WindowRight = "479";
            opml.Head = head;

            var outline = new Outline();
            outline.Text = "CNET News.com";
            outline.IsComment = "true";
            outline.IsBreakpoint = "true";
            outline.Created = DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT").ToUniversalTime();
            outline.Category.Add("/Harvard/Berkman");
            outline.Category.Add("/Politics");
            outline.Description = "Tech news and business reports by CNET News.com.";
            outline.HtmlUrl = "http://news.com.com/";
            outline.Language = "unknown";
            outline.Title = "CNET News.com";
            outline.Type = "rss";
            outline.Version = "RSS2";
            outline.XmlUrl = "http://news.com.com/2547-1_3-0-5.xml";

            var body = new Body();
            body.Outlines.Add(outline);
            opml.Body = body;

            var builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
            builder.Append("<opml version=\"2.0\">\r\n");
            builder.Append("<head>\r\n");
            builder.Append("<title>mySubscriptions.opml</title>\r\n");
            builder.Append("<dateCreated>Sat, 18 Jun 2005 12:11:52 GMT</dateCreated>\r\n");
            builder.Append("<dateModified>Tue, 02 Aug 2005 21:42:48 GMT</dateModified>\r\n");
            builder.Append("<ownerName>fnya</ownerName>\r\n");
            builder.Append("<ownerEmail>fnya@example.com</ownerEmail>\r\n");
            builder.Append("<ownerId>http://news.com.com/</ownerId>\r\n");
            builder.Append("<docs>http://news.com.com/</docs>\r\n");
            builder.Append("<expansionState>1,6,13,16,18,20</expansionState>\r\n");
            builder.Append("<vertScrollState>1</vertScrollState>\r\n");
            builder.Append("<windowTop>106</windowTop>\r\n");
            builder.Append("<windowLeft>106</windowLeft>\r\n");
            builder.Append("<windowBottom>558</windowBottom>\r\n");
            builder.Append("<windowRight>479</windowRight>\r\n");
            builder.Append("</head>\r\n");
            builder.Append("<body>\r\n");
            builder.Append("<outline ");
            builder.Append("text=\"CNET News.com\" ");
            builder.Append("isComment=\"true\" ");
            builder.Append("isBreakpoint=\"true\" ");
            builder.Append("created=\"Tue, 02 Aug 2005 21:42:48 GMT\" ");
            builder.Append("category=\"/Harvard/Berkman,/Politics\" ");
            builder.Append("description=\"Tech news and business reports by CNET News.com.\" ");
            builder.Append("htmlUrl=\"http://news.com.com/\" ");
            builder.Append("language=\"unknown\" ");
            builder.Append("title=\"CNET News.com\" ");
            builder.Append("type=\"rss\" ");
            builder.Append("version=\"RSS2\" ");
            builder.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" ");
            builder.Append("/>\r\n");
            builder.Append("</body>\r\n");
            builder.Append("</opml>");

            Assert.True(opml.ToString() == builder.ToString());

        }

        [Fact]
        public void CreateChildOpmlTest()
        {
            var opml = new Opml();
            opml.Encoding = "UTF-8";
            opml.Version = "2.0";

            var head = new Head();
            head.Title = "mySubscriptions.opml";
            opml.Head = head;

            var outline = new Outline();
            outline.Text = "IT";

            var childOutline = new Outline();
            childOutline.Text = "CNET News.com";
            childOutline.HtmlUrl = "http://news.com.com/";
            childOutline.XmlUrl = "http://news.com.com/2547-1_3-0-5.xml";

            outline.Outlines.Add(childOutline);

            var body = new Body();
            body.Outlines.Add(outline);
            opml.Body = body;

            var builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
            builder.Append("<opml version=\"2.0\">\r\n");
            builder.Append("<head>\r\n");
            builder.Append("<title>mySubscriptions.opml</title>\r\n");
            builder.Append("</head>\r\n");
            builder.Append("<body>\r\n");
            builder.Append("<outline text=\"IT\">\r\n");
            builder.Append("<outline text=\"CNET News.com\" ");
            builder.Append("htmlUrl=\"http://news.com.com/\" ");
            builder.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" />\r\n");
            builder.Append("</outline>\r\n");
            builder.Append("</body>\r\n");
            builder.Append("</opml>");

            Assert.True(opml.ToString() == builder.ToString());
        }
    }
}
