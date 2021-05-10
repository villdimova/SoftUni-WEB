using Microsoft.EntityFrameworkCore;
using MyFirstMvcApp.Controllers;
using MyFirstMvcApp.Data;
using SIS.Http;
using SIS.MvcFramework;
using System.Collections.Generic;

namespace MyFirstMvcApp
{
 public class Startup : IMvcApplication
    {
       
    public void ConfigureServices()
        {
           
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}