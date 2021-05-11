using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public interface IServiceCollection
    {
        void Add<TSource, TDestination>();

        object CreateInstance(Type type);
    }
}
