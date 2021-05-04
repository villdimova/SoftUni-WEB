using SIS.Http;
using System;
using System.Collections.Generic;


namespace SIS.MvcFramework
{
  public  interface IMvcApplication
    {
        void ConfigureServices();

        void Configure(List<Route> routeTable);
       
    }
}
