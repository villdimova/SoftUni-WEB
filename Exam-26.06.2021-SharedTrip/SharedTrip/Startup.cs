namespace SharedTrip
{
    using System.Threading.Tasks;
    using MyWebServer;
    using MyWebServer.Controllers;
    using SharedTrip.Data;
    using SharedTrip.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer.Results.Views;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<ApplicationDbContext>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>())
                  .WithConfiguration<ApplicationDbContext>(c => c.Database.Migrate())
                .Start();
    }
}
