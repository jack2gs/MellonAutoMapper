using System;
using System.Collections.Generic;

namespace MelonAutoMapper
{
    public class MapperConfigration: IMapperConfigration
    {
        private Dictionary<string, ITypeMapConfigration> _entryList = new Dictionary<string, ITypeMapConfigration>();
        public int EntryCount { get { return _entryList.Count; } }

        public ITypeMapExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            TypeMapExpression<TSource, TDestination> expression = new TypeMapExpression<TSource, TDestination>();

            _entryList.Add(typeof(TSource).FullName + "_" + typeof(TDestination).FullName, expression);

            return expression;
        }

        public ITypeMapConfigration GetTypeMapConfigration<TSource, TDestination>()
        {
            string key = typeof(TSource).FullName + "_" + typeof(TDestination).FullName;
            if (_entryList.ContainsKey(key))
            {
                return _entryList[key];
            }
            else
            {
                return null;
            }
        }
    }
}