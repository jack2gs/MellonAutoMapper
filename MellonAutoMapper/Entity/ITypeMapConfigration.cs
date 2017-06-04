using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MelonAutoMapper
{
    public interface ITypeMapConfigration
    {
        Type SourceType { get; set; }
        Type DestinationType { get; set; }
        LambdaExpression GetFunction(PropertyInfo destinationPropertyInfo);
    }
}