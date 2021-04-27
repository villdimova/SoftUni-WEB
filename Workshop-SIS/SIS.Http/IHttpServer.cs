
using System;
using System.Threading.Tasks;

namespace SIS.Http
{
   public interface IHttpServer
    {

        void AddRoute(string path, Func<HttpRequest, HttpResponse> action);

        Task StartAsync(int port);
    }
}
