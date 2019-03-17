using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InfoTrack.KeywordRank.Services
{
    public class KeywordSearchService : IKeywordSearchService
    {
        private readonly IGoogleService _googleService;
        private readonly IContentParser _contentParser;
        private readonly ILogger<KeywordSearchService> _logger;

        public KeywordSearchService(
          IGoogleService googleService,
          IContentParser contentParser,
        ILogger<KeywordSearchService> logger)
        {
            this._googleService = googleService;
            this._contentParser = contentParser;
            this._logger = logger;
        }


        /// <summary>
        /// Gets the occurences of a <paramref name="url"/> when searching for a <paramref name="keyword"/>.
        /// </summary>
        /// <returns>The occurence.</returns>
        /// <param name="keyword">Keyword to search for.</param>
        /// <param name="url">Url to be extracted from results.</param>
        public async Task<int> GetOccurence(string keyword, string url)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new ArgumentNullException(nameof(keyword));

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var searchResult = string.Empty;
            try
            {
                searchResult = await _googleService.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new IOException("An error occured while fetching data from google", ex);
            }

            _logger.LogInformation(searchResult);
            return _contentParser.Parse(searchResult, url);
        }
    }
}
