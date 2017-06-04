using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelonAutoMapper
{
    public interface IMapper
    {
        void Initialize(Action<MapperConfigration> mca);

        TDestination Map<TSource, TDestination>(TSource source) where TDestination: class,new();
    }
}
