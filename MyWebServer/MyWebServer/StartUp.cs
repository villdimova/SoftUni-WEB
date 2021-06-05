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
                .MapGet("/Cats", request =>
                {
                    var query = request.Query;

                    var catName = query.ContainsKey("Name")
                            ? query["Name"]
                            : "the cats";

                    var result = $"<h1>Hello from {catName}!</h1>";

                  return  new HtmlResponse(result);
                })
               
                .MapGet("/Dogs",new HtmlResponse("<h1>Hello from the dogs!</h1>")))
            .Start();

           
    }
}
