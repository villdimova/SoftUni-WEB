using MyWebServer.Http;

namespace MyWebServer.Results
{
    class UnauthorizedResult:ActionResult
    {
        public UnauthorizedResult(HttpResponse response)
           : base(response)
           => this.StatusCode = HttpStatusCode.Unauthorized;
    }
}
