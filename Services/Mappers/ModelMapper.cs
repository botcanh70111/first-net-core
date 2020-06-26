using AutoMapper;

namespace Services.Mappers
{
    public class ModelMapper : IModelMapper
    {
        private readonly IMapper _mapper;

        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }

        public TDestination Map<T, TDestination>(T source, TDestination rootModel)
        {
            return _mapper.Map<T, TDestination>(source, rootModel);
        }
    }
}
