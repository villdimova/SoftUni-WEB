namespace MyWebServer
{
    using MyWebServer.Server;
    using MyWebServer.Server.Responses;
    using System.Threading.Tasks;
    public class StartUp
    {
        public static async Task Main()
            =>await new HttpServer(9090,routes=>routes
                .MapGet("/",new TextResponse("Hello there!"))
                .MapGet("/Cats",new HtmlResponse("<h1>Hello from the cats!</h1>"))
                .MapGet("/Dogs",new HtmlResponse("<h1>Hello from the dogs!</h1>")))
            .Start();

           
    }
}
