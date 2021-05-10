using SIS.Http;
using SIS.MvcFramework;
using System;

namespace MyFirstMvcApp.Controllers
{
   public class UsersController: Controller
    {

       public HttpResponse Login()
        {
            return this.View();
           
        }

        public HttpResponse Register()
        {
            return this.View();
           
        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            //TODO: read data
            //TODO: check user
            //TODO: log user

            return this.Redirect("/");
         


        }
    }
}
