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
                .MapGet<AnimalsController>("/Cats",c=> c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<HomeController>("/Google",c=>c.ToGoogle())
               .MapGet<HomeController>("/ToCats", c => c.LocalRedirect()))
            .Start();

           
    }
}
