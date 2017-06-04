using System;
using System.Linq.Expressions;

namespace MelonAutoMapper
{
    public interface ITypeMapExpression<TSource, TDestination>
    {
        ITypeMapExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination,TMember>> exp, Expression<Func<TSource, TMember>> func);
    }
}