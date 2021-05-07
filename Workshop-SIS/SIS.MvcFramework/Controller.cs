﻿using SIS.Http;
using SIS.MvcFramework.ViewEngine;
using System.Runtime.CompilerServices;
using System.Text;


namespace SIS.MvcFramework
{
   public abstract class Controller
    {
        private SISViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SISViewEngine();
        }

        public HttpResponse View(object viewModel=null,[CallerMemberName]string viewPath=null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "__VIEW____GOES___HERE__");
            layout = this.viewEngine.GetHtml(layout, viewModel,null);
            var viewContent = System.IO.File.ReadAllText(
                "Views/" + 
                this.GetType().Name.Replace("Controller",string.Empty)+ 
                "/"+
                viewPath +
                ".cshtml");

            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, null);
            var responseHtml = layout.Replace("__VIEW____GOES___HERE__", viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;

        }

        public HttpResponse File(string filePath,string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;

        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location",url));

            return response;
        }
    }
}
