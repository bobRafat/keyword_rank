using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InfoTrack.KeywordRank.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly string numberOfResults = "10";

        public GoogleService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            numberOfResults = configuration.GetValue<string>("google:numberOfResults");
        }


        public async Task<string> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new ArgumentNullException(nameof(keyword));
            if (keyword.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(keyword));
            keyword = Uri.EscapeUriString(keyword);

            var request = new HttpRequestMessage(HttpMethod.Get, $"?q={keyword}&&gws_rd=cr&num={numberOfResults}");

            var client = _clientFactory.CreateClient("google");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}