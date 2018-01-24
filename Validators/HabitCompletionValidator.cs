using System.Collections.Generic;
using HealthyHabits.Models;
using HealthyHabits.Dtos;
using HealthyHabits.Repositories;
using System.Security.Claims;

namespace HealthyHabits.Validators
{
    public class HabitCompletionValidator : BaseValidator<HabitCompletion>
    {
        private HabitRepository _habitRepo;

        public HabitCompletionValidator(HabitRepository habitRepo)
        {
            _habitRepo = habitRepo;
        }

        protected override IEnumerable<ValidationError> GetCustomValidationErrorsForCreate(HabitCompletion model, ClaimsPrincipal user)
        {
            var answer = new List<ValidationError>();

            // todo: figure out how to constructor-inject the user instead
            var habit = _habitRepo.GetSingleItem(model.HabitId, user.Identity.Name);
            if ((habit == null) || (habit.Id != model.HabitId))
            {
                answer.Add(new ValidationError() { ErrorCode = "HABIT_INACCESSIBLE", Message = "The HabitId property must refer to an accessible Habit." });
            }

            return answer;
        }

        protected override IEnumerable<ValidationError> GetCustomValidationErrorsForUpdate(HabitCompletion existingModel, HabitCompletion newModel, ClaimsPrincipal user)
        {
            var answer = new List<ValidationError>();

            if ((newModel.HabitId <= 0) || (existingModel.HabitId != newModel.HabitId))
            {
                answer.Add(new ValidationError() { ErrorCode = "HABIT_MAY_NOT_CHANGE", Message = "The HabitId property may not change during update." });
            }

            return answer;
        }
    }
}