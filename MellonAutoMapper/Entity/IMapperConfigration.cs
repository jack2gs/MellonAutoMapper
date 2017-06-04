namespace MelonAutoMapper
{
    public interface IMapperConfigration
    {
        int EntryCount { get; }

        ITypeMapExpression<TSource, TDestination> CreateMap<TSource, TDestination>();

        ITypeMapConfigration GetTypeMapConfigration<TSource, TDestination>();
    }
}