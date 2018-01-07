using HealthyHabits.Models;
using HealthyHabits.Dtos;

namespace HealthyHabits.Translators
{
    public class BaseTranslator<TModel, TDto>
        where TModel : BaseModel
        where TDto : BaseDto<TModel>
    {
        public TDto Translate(TModel model)
        {
            return default(TDto);
        }

        public TModel Translate(TDto dto)
        {
            return null;
        }
    }
}