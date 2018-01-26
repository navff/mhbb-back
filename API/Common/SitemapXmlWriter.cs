using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace Camps.Api.Utils
{
    public class SitemapXmlWriter
    {
        public MemoryStream CreateXml(IEnumerable<SitemapUrl> urls)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.Encoding = Encoding.UTF8;
            settings.NewLineChars = Environment.NewLine;

            MemoryStream localMemoryStream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(localMemoryStream, settings))
            {
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                foreach (var url in urls)
                {
                    WriteUrl(writer,
                             url.Url,
                             url.Priority,
                             url.Modified,
                             url.ChangeFreq);
                }
                writer.WriteEndElement();
                writer.Flush();
            }
            return localMemoryStream;
        }

        private void WriteUrl(XmlWriter writer, string url,
                              double priority, DateTimeOffset lastmod,
                              string changefreq)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            writer.WriteStartElement("url");
            writer.WriteElementString("loc", url);
            writer.WriteElementString("lastmod", lastmod.Date.ToString("yyyy-MM-dd"));
            writer.WriteElementString("changefreq", changefreq);
            writer.WriteElementString("priority", priority.ToString(nfi));
            writer.WriteEndElement();
        }
    }

    public class SitemapUrl
    {
        public string Url { get; set; }
        public double Priority { get; set; }
        public DateTimeOffset Modified { get; set; }
        public string ChangeFreq { get; set; }

    }

    public static class SitemapPagePriority
    {
        public const double HIGH = 1;
        public const double MEDIUM = 0.6;
        public const double LOW = 0.3;
    }

    public static class SitemapPageChangeFrec
    {
        public const string ALWAYS = "always";
        public const string HOURLY = "hourly";
        public const string DAILY = "daily";
        public const string WEEKLY = "weekly";
        public const string MONTHLY = "monthly";
        public const string YEARLY = "yearly";
        public const string NEVER = "never";
    }

}