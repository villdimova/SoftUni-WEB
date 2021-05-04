﻿using SIS.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public static class Host
    {

        public static async Task CreateHostAsync(IMvcApplication application, int port=80)
        {
            List<Route> routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

      
    }
}
