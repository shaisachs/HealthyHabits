using System.Collections.Generic;

namespace HealthyHabits.Models
{
    public class BaseModelCollection<T> where T : BaseModel
    {
        public IEnumerable<T> Items { get; set; }
    }
}