using Fusillade;
using System.Net.Http;
using System.Threading.Tasks;

namespace Helper.Service
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);

    }
}
