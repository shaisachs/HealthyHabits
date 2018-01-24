using System.ComponentModel.DataAnnotations;

namespace HealthyHabits.Models
{
    public class Habit : BaseModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}