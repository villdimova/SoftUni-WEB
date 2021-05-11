using MyFirstMvcApp.ViewModels;
using SIS.Http;
using SIS.MvcFramework;
using System;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            return this.View();
        }
    }
}
