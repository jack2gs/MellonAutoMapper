using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace MelonAutoMapper
{
    public class TypeMapExpression<TSource, TDestination>: ITypeMapExpression<TSource,TDestination>, ITypeMapConfigration
    {
        Dictionary<PropertyInfo, LambdaExpression> _items;
        public TypeMapExpression()
        {
            _items = new Dictionary<PropertyInfo, LambdaExpression>();

            Type desctinationType = typeof(TDestination);
            Type sourceType = typeof(TSource);

            PropertyInfo[] descPis = desctinationType.GetProperties();
            PropertyInfo[] sourcePis = sourceType.GetProperties();

            foreach(var pi in descPis)
            {
                PropertyInfo sourcePi = sourcePis.FirstOrDefault(e => e.Name == pi.Name&&e.PropertyType==pi.PropertyType);
                if (sourcePi != null)
                {
                    // build the lambda expression 
                    ParameterExpression a = Expression.Parameter(typeof(TSource), "src");
                    MemberExpression me = Expression.MakeMemberAccess(a, sourcePi);
                    var lambda = Expression.Lambda(me, a);
                    _items.Add(pi, lambda);
                }
            }
        }

        public int ItemCount
        {
            get
            {
                return _items.Count;
            }
        }

        public Type SourceType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Type DestinationType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ITypeMapExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> exp, Expression<Func<TSource,TMember>> func)
        {
            PropertyInfo pi = (PropertyInfo)((MemberExpression)exp.Body).Member;
            _items.Add(pi, func);

            return this;
        }

        public LambdaExpression GetFunction(PropertyInfo destinationPropertyInfo)
        {
            if(_items.ContainsKey(destinationPropertyInfo))
            {
                return _items[destinationPropertyInfo];
            }
            else
            {
                return null;
            }
        }
    }
}
