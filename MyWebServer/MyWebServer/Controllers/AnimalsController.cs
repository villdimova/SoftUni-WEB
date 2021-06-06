namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Responses;

    public class AnimalsController: Controller
    {
        public AnimalsController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse Cats()
        {
            var query = this.Request.Query;

            var catName = query.ContainsKey("Name")
                    ? query["Name"]
                    : "the cats";

            var result = $"<h1>Hello from {catName}!</h1>";

            return Html(result);
        }

        public HttpResponse Dogs()
            => Html("<h1>Hello from the dogs!</h1>");
    }
}
