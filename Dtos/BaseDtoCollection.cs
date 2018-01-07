using System.Collections.Generic;
using HealthyHabits.Models;

namespace HealthyHabits.Dtos
{
    public class BaseDtoCollection<TModel, TDto>
        where TModel : BaseModel
        where TDto : BaseDto<TModel>
    {
        public IEnumerable<TDto> Items { get; set; }
    }
}