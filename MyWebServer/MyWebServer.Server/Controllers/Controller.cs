namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Responses;

    public abstract class Controller
    {

        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }
        protected  HttpRequest  Request { get; init; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);
    }
}
