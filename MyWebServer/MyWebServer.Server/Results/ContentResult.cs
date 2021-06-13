namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using System.Text;

    public class ContentResult : ActionResult
    {
        public ContentResult(
            HttpResponse response,
            string content, 
            string contentType)
           : base(response)
           => this.PrepareContent(content, contentType);

    }
}
