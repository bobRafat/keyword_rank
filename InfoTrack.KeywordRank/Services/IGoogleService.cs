using System.Threading.Tasks;

namespace InfoTrack.KeywordRank.Services
{
    public interface IGoogleService
    {
        Task<string> Search(string keyword);
    }
}