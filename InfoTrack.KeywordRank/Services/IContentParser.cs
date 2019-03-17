using System.Threading.Tasks;

namespace InfoTrack.KeywordRank.Services
{
    public interface IContentParser
    {
        int Parse(string searchResult, string url);
    }
}