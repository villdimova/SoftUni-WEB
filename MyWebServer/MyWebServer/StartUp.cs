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
                .MapGet<HomeController>("/Error", c => c.Error())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<AccountController>("/Cookies",c=>c.CookiesCheck())
                .MapGet<AccountController>("/Session",c=>c.SessionsCheck())
                .MapGet<AccountController>("/Login",c=>c.Login())
                .MapGet<AccountController>("/Logout",c=>c.Logout())
                .MapGet<AccountController>("/Authentication", c=>c.AuthenticationCheck())
                .MapGet<CatsController>("/Cats/Create", c => c.Create())
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();

           
    }
}
