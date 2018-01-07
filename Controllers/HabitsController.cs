using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using HealthyHabits.Models;

namespace HealthyHabits.Controllers
{
    [Route("api/v1/habits")]
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class HabitsController : BaseController<Habit>
    {
        public HabitsController(HealthyHabitsContext context) :
            base(context, (c) => c.Habits, "GetHabit")
        {
        }

        [HttpGet]
        public BaseModelCollection<Habit> GetAll()
        {
            return base.GetAllBase();
        }

        [HttpGet("{id}", Name = "GetHabit")]
        public IActionResult GetById(long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Habit item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Habit newItem)
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