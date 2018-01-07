using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using HealthyHabits.Models;
using HealthyHabits.Dtos;
using HealthyHabits.Translators;

namespace HealthyHabits.Controllers
{
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class HabitCompletionsController : BaseController<HabitCompletion, HabitCompletionDto>
    {
        public HabitCompletionsController(HealthyHabitsContext context,
            BaseTranslator<HabitCompletion, HabitCompletionDto> translator) :
            base(context, (c) => c.HabitCompletions, "GetHabitCompletion", translator)
        {
        }

        [HttpGet("api/v1/habits/{habitId}/completions")]
        public BaseDtoCollection<HabitCompletion, HabitCompletionDto> GetAll(long habitId)
        {
            return base.GetAllBase(additionalFilter: (h) => h.HabitId == habitId );
        }

        [HttpGet("api/v1/habits/{habitId}/completions/{id}", Name = "GetHabitCompletion")]
        public IActionResult GetById(long habitId, long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost("api/v1/habits/{habitId}/completions")]
        public IActionResult Create(long habitId, [FromBody] HabitCompletionDto item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("api/v1/habits/{habitId}/completions/{id}")]
        public IActionResult Update(long habitId, long id, [FromBody] HabitCompletionDto newItem)
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