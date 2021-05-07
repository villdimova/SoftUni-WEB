using SIS.Http;
using System;

namespace SIS.MvcFramework
{
    public abstract class BaseHttpAttribute: Attribute
    {
        public string Url { get; set; }

        public abstract HttpMethod Method { get; }
    }


}

