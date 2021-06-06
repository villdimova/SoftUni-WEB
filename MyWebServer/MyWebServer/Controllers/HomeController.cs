namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Responses;

    public  class HomeController: Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
            => Text("Hello there!");

        public HttpResponse LocalRedirect() => Redirect("/Cats");

        public HttpResponse ToGoogle() => Redirect("https://www.google.bg");


    }
}
