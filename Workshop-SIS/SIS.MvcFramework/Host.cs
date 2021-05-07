﻿using SIS.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public static class Host
    {

        public static async Task CreateHostAsync(IMvcApplication application, int port=80)
        {
            List<Route> routeTable = new List<Route>();
            AutoRegisterStaticFiles(routeTable);
            AutoRegisterRoutes(routeTable,application);

            application.ConfigureServices();
            application.Configure(routeTable);

            Console.WriteLine("All registered routes:");
            foreach (var route in routeTable)
            {
                Console.WriteLine($"{route.Method} => {route.Path}");
            }
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcApplication application)
        {
            var controllerTypes=application.GetType().Assembly.GetTypes()
                 .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));
            foreach (var controllerType in controllerTypes)
            {
               
                var methods = controllerType.GetMethods()
                    .Where(x => x.IsPublic && !x.IsStatic && x.DeclaringType == controllerType
                    && !x.IsAbstract && !x.IsConstructor && !x.IsSpecialName);
                foreach (var method in methods)
                {
                    var url = "/" + controllerType.Name.Replace("Controller", string.Empty)
                        + "/" + method.Name;
                    var attribute = method.GetCustomAttributes(false)
                        .Where(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                        .FirstOrDefault() as BaseHttpAttribute ;

                    if (!string.IsNullOrEmpty (attribute?.Url))
                    {
                        url = attribute.Url;

                    }

                    var httpMethod = HttpMethod.Get;

                    if (attribute!=null)
                    {
                        httpMethod = attribute.Method;
                    }

                    routeTable.Add(new Route(url,httpMethod, (request) =>
                     {

                         var instance = Activator.CreateInstance(controllerType);
                         var response = method.Invoke(instance, new[] { request }) as HttpResponse;
                         return response;
                     }));
                   
                }
            }
        }

        private static void AutoRegisterStaticFiles(List<Route> routeTable) 
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var staticFile in staticFiles)
            {
                var url = staticFile.Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");
                routeTable.Add(new Route(url, HttpMethod.Get, (request) =>
                {
                    var fileContent = File.ReadAllBytes(staticFile);
                    var fileExt = new FileInfo(staticFile).Extension;
                    var contentType = fileExt switch
                    {
                        ".txt" => "text/plain",
                        ".js" => "text/javascript",
                        ".css" => "text/css",
                        ".jpeg" => "image/jpg",
                        ".jpg" => "image/jpg",
                        "png" => "image/png",
                        ".gif" => "image/gif",
                        ".ico" => "image/vnd.microsoft.icon",
                        ".html" => "text/html",
                        _ => "text/plain",

                    };
                    return new HttpResponse(contentType, fileContent, HttpStatusCode.Ok);


                }));
            }
        }
      
    }
}