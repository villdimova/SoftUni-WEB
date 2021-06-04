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
                .MapGet("/Cats",new HtmlResponse("Hello from the cats!"))
                .MapGet("/Dogs",new HtmlResponse("Hello from the dogs!")))
            .Start();

           
    }
}
