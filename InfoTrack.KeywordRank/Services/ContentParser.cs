using System;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace InfoTrack.KeywordRank.Services
{
    public class ContentParser : IContentParser
    {
        /// <summary>
        /// Parse the specified content and returns the number of times the url is found.
        /// </summary>
        /// <returns>The number of occurences of <paramref name="url"/>.</returns>
        /// <param name="searchResult">The content to parse.</param>
        /// <param name="url">The url to look for.</param>
        public int Parse(string searchResult, string url)
        {
            if (string.IsNullOrEmpty(searchResult))
                throw new ArgumentNullException(nameof(searchResult));

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));


            var doc = new XmlDocument();

            var occurence = 0;
            try
            {
                searchResult = SanitizeContent(searchResult);

                doc.LoadXml(searchResult);
                var nodeList = doc.SelectNodes("//div[@class='rc']");

                if (nodeList.Count == 0)
                    throw new FormatException("There are some issues in the result received from google, it may be due to excessive number of requests to google or your IP is being blocked by google.");

                for (int i = 0; i < nodeList.Count; i++)
                {
                    var node = nodeList.Item(i);
                    var link = node.SelectSingleNode("div[@class=\"r\"]/a/@href");
                    if (link.Value.Contains(url))
                        occurence++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while parsing the content returned from google!", ex);
            }
            return occurence;
        }

        private static string SanitizeContent(string searchResult)
        {
            searchResult = searchResult.Replace("doctype", "DOCTYPE").Replace("&&", "").Replace("&", "");
            searchResult = Regex.Replace(searchResult, "<head.*?</head>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            searchResult = Regex.Replace(searchResult, "<noscript.*?</noscript>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            while(Regex.Match(searchResult, "<script.*?</script>").Success)
            {
                searchResult = Regex.Replace(searchResult, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
           
            while (Regex.Match(searchResult, @"<input(\s+[^>]*)?>").Success)
            {
                searchResult = Regex.Replace(searchResult, @"<input(\s+[^>]*)?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }

            searchResult = Regex.Replace(searchResult, @"<img(\s+[^>]*)?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            searchResult = Regex.Replace(searchResult, @"<br(\s+[^>]*)?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            searchResult = Regex.Replace(searchResult, @"<wbr(\s+[^>]*)?>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return searchResult;
        }
    }
}