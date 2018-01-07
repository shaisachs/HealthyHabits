using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using HealthyHabits.Models;

namespace HealthyHabits.Controllers
{
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class HabitCompletionsController : BaseController<HabitCompletion>
    {
        public HabitCompletionsController(HealthyHabitsContext context) :
            base(context, (c) => c.HabitCompletions, "GetHabitCompletion")
        {
        }

        [HttpGet("api/v1/habits/{habitId}/completions")]
        public BaseModelCollection<HabitCompletion> GetAll(long habitId)
        {
            return base.GetAllBase(additionalFilter: (h) => h.HabitId == habitId );
        }

        [HttpGet("api/v1/habits/{habitId}/completions/{id}", Name = "GetHabitCompletion")]
        public IActionResult GetById(long habitId, long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost("api/v1/habits/{habitId}/completions")]
        public IActionResult Create(long habitId, [FromBody] HabitCompletion item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("api/v1/habits/{habitId}/completions/{id}")]
        public IActionResult Update(long habitId, long id, [FromBody] HabitCompletion newItem)
        {
            return base.UpdateBase(id, newItem);
        }

        [HttpDelete("api/v1/habits/{habitId}/completions/{id}")]
        public IActionResult Delete(long habitId, long id)
        {
            return base.DeleteBase(id);
        }
        
        protected override HabitCompletion UpdateExistingItem(HabitCompletion existingItem, HabitCompletion newItem) 
        {
            existingItem.Completed = newItem.Completed;

            return existingItem;
        }

    }
}