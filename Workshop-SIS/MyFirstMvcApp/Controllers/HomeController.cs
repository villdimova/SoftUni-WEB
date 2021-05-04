using SIS.Http;
using SIS.MvcFramework;


namespace MyFirstMvcApp.Controllers
{
  public  class HomeController: Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

      
    }
}
