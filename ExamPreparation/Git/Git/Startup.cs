namespace Git
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using System.Threading.Tasks;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using Git.Services;

    public class Startup 
    {
        public static async Task Main()
           => await HttpServer
               .WithRoutes(routes => routes
                   .MapStaticFiles()
                   .MapControllers())
               .WithServices(services => services
                   .Add<IViewEngine, CompilationViewEngine>()
                   .Add<GitDbContext>()
                    .Add<IValidator,Validator>()
                    .Add<IPasswordHasher,PasswordHasher>())
                  .WithConfiguration<GitDbContext>(c => c.Database.Migrate())
               .Start();
    }
}
