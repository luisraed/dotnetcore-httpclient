using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stems.Services
{
    public interface ISteamsFactory
    {
        Task<IEnumerable<string>> CreateSteams(string stem);
    }
    public class SteamsFactory : ISteamsFactory
    {
        private readonly IGitHubClient _gitHubClient;

        public SteamsFactory(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<IEnumerable<string>> CreateSteams(string stem)
        {
            if (string.IsNullOrEmpty(stem))
                return new List<string>();

            var githubData = await _gitHubClient.GetDataAsync();

            return githubData?.Where(x => x.ToLowerInvariant().StartsWith(stem.ToLowerInvariant()));
        }
    }
}
