namespace MyWebServer
{
    using MyWebServer.Controllers;
    using MyWebServer.Server;
    using System.Threading.Tasks;
    using MyWebServer.Server.Controllers;

    public class StartUp
    {
        public static async Task Main()
            =>await new HttpServer(9090,routes=>routes
              .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/ToCats", c => c.LocalRedirect())
                .MapGet<HomeController>("/Google", c => c.ToGoogle())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<AccountController>("/Cookies",c=>c.ActionWithCookies())
                .MapGet<CatsController>("/Cats/Create", c => c.Create())
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();

           
    }
}
