using System.Threading.Tasks;

namespace InfoTrack.KeywordRank.Services
{
    public interface IKeywordSearchService
    {
        Task<int> GetOccurence(string keyword, string url);
    }
}