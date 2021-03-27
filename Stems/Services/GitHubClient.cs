using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stems.Services
{
    public interface IGitHubClient
    {
        Task<IEnumerable<string>> GetDataAsync();
    }
    public class GitHubClient : IGitHubClient
    {
        private readonly IConfiguration _configuration;

        public GitHubClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<string> Data { get; set; }
        public async Task<IEnumerable<string>> GetDataAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_configuration["Services:EnglishWordsUrl"]);

                if (Data == null)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Data = responseBody.Split("\r\n");
                }
            };

            return Data;
        }
    }
}
