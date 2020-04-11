using System.Threading.Tasks;
using TidBit.Services.Responses;

namespace TidBit.Interfaces
{
    public interface ITBService
    {
        Task<ArticlesRootObject> GetAllArticles();
    }
}
