using SIS.Http;

namespace SIS.MvcFramework
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {

        }

        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }
        public override HttpMethod Method => HttpMethod.Get;

    }


}

