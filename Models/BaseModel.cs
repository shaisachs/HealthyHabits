using System;

namespace HealthyHabits.Models
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
    }
}