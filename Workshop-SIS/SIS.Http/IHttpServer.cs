
using System;
using System.Threading.Tasks;

namespace SIS.Http
{
   public interface IHttpServer
    {

        Task StartAsync(int port);
    }
}
