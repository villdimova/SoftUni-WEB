namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;

    public class HtmlResult : ActionResult
    {
        public HtmlResult(HttpResponse response,string html) 
            : base(response)
        {
        }
    }
}
