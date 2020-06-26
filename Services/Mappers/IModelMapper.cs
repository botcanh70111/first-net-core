namespace Services.Mappers
{
    public interface IModelMapper
    {
        T Map<T>(object source);
        TDestination Map<T, TDestination>(T source, TDestination rootModel);
    }
}
