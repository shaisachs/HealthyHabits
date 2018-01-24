using HealthyHabits.Models;
using HealthyHabits.Dtos;
using AutoMapper;

namespace HealthyHabits.Translators
{
    public class BaseTranslator<TModel, TDto>
        where TModel : BaseModel
        where TDto : BaseDto<TModel>
    {
        private readonly IMapper _mapper;
        public BaseTranslator(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public TDto Translate(TModel model)
        {
            return _mapper.Map<TDto>(model);
        }

        public TModel Translate(TDto dto)
        {
            TModel model = null;
            try
            {
                model = _mapper.Map<TModel>(dto);
            }
            catch (AutoMapperMappingException)
            {
                model = null;
            }

            return model;
        }
    }
}