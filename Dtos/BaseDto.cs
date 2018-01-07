using HealthyHabits.Models;

namespace HealthyHabits.Dtos
{
    public class BaseDto<T> where T : BaseModel
    {
        public long Id { get; set; }
    }
}