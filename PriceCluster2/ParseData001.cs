using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PriceCluster2
{
    public class ParseData001
    {
        public string DocumentUrl { get; set; }
        public decimal Price { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<string> NumericProperties { get; set; }

        public ParseData001(string documentUrl)
        {
            DocumentUrl = documentUrl;
            Parse();
        }

        public HtmlDocument GetHtmlDocument(string documentUrl)
        {
            var doc = new HtmlDocument();
            doc.Load(documentUrl);
            return doc;
        }

        public HtmlNode GetWeightNode(HtmlDocument doc)
        {
            return
                doc.DocumentNode.Descendants("div")
                    .Where(x => x.InnerText == "Produktvekt").First();
        }

        public HtmlNode GetPriceNode(HtmlDocument doc)
        {
            return
                doc.DocumentNode.Descendants("div")
                    .Where(x => string.Join("", x.GetClasses()).Contains("TabLowestPrice")).First();
        }

        public decimal GetPrice(HtmlNode node)
        {
            var rex = new Regex(@"[0-9\.]*");
            return decimal.Parse(string.Join("", rex.Matches(node.InnerText).Select(x => x.Value)));
        }

        public string GetPropertyClass(HtmlNode node) {
            return node.GetClasses().First();
        }

        public Dictionary<string, string> GetProperties(HtmlDocument doc, string propertyClass) {
            var ret = new Dictionary<string, string>();
            string key = null;
            foreach (var node in doc.DocumentNode.Descendants("div").Where(x => x.HasClass(propertyClass)))
            {
                if (key == null)
                {
                    key = node.InnerText;
                } else
                {
                    var value = node.InnerText;
                    if (!ret.ContainsKey(key))
                    {
                        ret.Add(key, value);
                    }
                    key = null;
                }
            }
            return ret;
        }

        public List<string> GetNumericProperties(Dictionary<string, string> properties)
        {
            var ret = new List<string>();
            var rex = new Regex(@"^[0-9\.]+");
            foreach (var prop in properties)
            {
                if (rex.IsMatch(prop.Value))
                {
                    ret.Add(prop.Key);
                }
            }
            return ret;
        }

        public void Parse()
        {
            var doc = GetHtmlDocument(DocumentUrl);

            {
                var priceNode = GetPriceNode(doc);
                var price = GetPrice(priceNode);
                Price = price;
            }

            {
                var weightNode = GetWeightNode(doc);
                var propertyClass = GetPropertyClass(weightNode);
                Console.WriteLine("PROP", propertyClass);
                var properties = GetProperties(doc, propertyClass);
                var numericProperties = GetNumericProperties(properties);
                Properties = properties;
                NumericProperties = numericProperties;
            }

            Properties["LavestePris"] = Price.ToString();
            NumericProperties.Add("LavestePris");
        }
    }
}
