using System;
using System.Linq.Expressions;

namespace MelonAutoMapper
{
    public interface ITypeConfigration<TSource, TDestination>
    {
        ITypeConfigration<TSource, TDestination> ForMember<TMember>(Expression<Func<TSource,TMember>> exp);
    }
}