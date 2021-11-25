using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapster
{
    public static class TypeAdapterExtensions
    {
        public static TDestination Adapt<TDestination>(this object source, Action<TDestination> action)
        {
            var target = source.Adapt<TDestination>();
            if (target != null)
            {
                action?.Invoke(target);
            }
            return target;
        }
    }
}
