using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using HealthyHabits.Models;
using HealthyHabits.Dtos;
using HealthyHabits.Translators;
using HealthyHabits.Repositories;
using HealthyHabits.Validators;

namespace HealthyHabits.Controllers
{
    [Route("api/v1/habits")]
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class HabitsController : BaseController<Habit, HabitDto>
    {
        public HabitsController(
            BaseTranslator<Habit, HabitDto> translator,
            BaseValidator<Habit> validator,
            HabitRepository repo,
            ValidationErrorTranslator errorTranslator) :
            base("GetHabit", translator, validator, repo, errorTranslator)
        {
        }

        [HttpGet]
        public BaseDtoCollection<Habit, HabitDto> GetAll()
        {
            return base.GetAllBase();
        }

        [HttpGet("{id}", Name = "GetHabit")]
        public IActionResult GetById(long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] HabitDto item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] HabitDto newItem)
        {
            return base.UpdateBase(id, newItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return base.DeleteBase(id);
        }
        
        protected override Habit UpdateExistingItem(Habit existingItem, Habit newItem) 
        {
            existingItem.Name = newItem.Name;

            return existingItem;
        }

    }
}