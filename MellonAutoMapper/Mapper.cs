using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MelonAutoMapper
{
    public class Mapper:IMapper
    {
        private MapperConfigration _config;

        public MapperConfigration Configration {
            get {
                return _config;
            }
        }

        public void Initialize(Action<MapperConfigration> mca)
        {
            if (_config == null)
            {
                _config = new MapperConfigration();
            }

            mca(_config);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class,new()
        {
            TDestination des = null;

            ITypeMapConfigration cfg =  _config.GetTypeMapConfigration<TSource, TDestination>();

            if (cfg!=null)
            {
                des = new TDestination();
                PropertyInfo[] piArray = typeof(TDestination).GetProperties();

                foreach(PropertyInfo pi in piArray)
                {
                    var func = cfg.GetFunction(pi);
                    if (func!=null)
                    {
                        pi.SetValue(des, func.Compile().DynamicInvoke(source));
                    }
                }
            }

            return des;
        }
    }
}
