using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HealthyHabits.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HealthyHabits.Dtos;
using HealthyHabits.Translators;

namespace HealthyHabits.Controllers
{
    public abstract class BaseController<TModel, TDto> : Controller
        where TModel : BaseModel
        where TDto : BaseDto<TModel>
    {
        private readonly HealthyHabitsContext _context;
        private readonly Func<HealthyHabitsContext, DbSet<TModel>> _dbsetGetter;
        private readonly string _getSingularRouteName;
        private readonly BaseTranslator<TModel, TDto> _translator;

        public BaseController(HealthyHabitsContext context, 
            Func<HealthyHabitsContext, DbSet<TModel>> dbsetGetter,
            string getSingularRouteName,
            BaseTranslator<TModel, TDto> translator)
        {
            _context = context;
            _dbsetGetter = dbsetGetter;
            _getSingularRouteName = getSingularRouteName;
            _translator = translator;
        }

        protected BaseDtoCollection<TModel, TDto> GetAllBase(Func<TModel, bool> additionalFilter = null)
        {
            Func<TModel, bool> predicate =
                (t) => IsOwnedByCurrentUser(t) &&
                    (additionalFilter == null ? true : additionalFilter(t));

            var models = _dbsetGetter(_context).Where(predicate).ToList();
            var dtos = from model in models select _translator.Translate(model);
            var answer = new BaseDtoCollection<TModel, TDto>() { Items = dtos };

            return answer;
        }

        protected IActionResult GetByIdBase(long id)
        {
            var model = GetSingleItem(id);

            if (model == null)
            {
                return NotFound();
            }

            return new ObjectResult(_translator.Translate(model));
        }

        protected IActionResult CreateBase(TDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var model = _translator.Translate(dto);

            model.Created = DateTime.Now;
            model.Creator = CurrentUserName();

            _dbsetGetter(_context).Add(model);
            _context.SaveChanges();

            return CreatedAtRoute(_getSingularRouteName, new { id = model.Id }, _translator.Translate(model));
        }

        protected IActionResult UpdateBase(long id, TDto newDto)
        {
            if (newDto == null || newDto.Id != id)
            {
                return BadRequest();
            }

            var newModel = _translator.Translate(newDto);

            var existingModel = GetSingleItem(id);

            if (existingModel == null)
            {
                return NotFound();
            }

            existingModel = UpdateExistingItem(existingModel, newModel);

            _dbsetGetter(_context).Update(existingModel);
            _context.SaveChanges();
            return new NoContentResult();
        }

        protected IActionResult DeleteBase(long id)
        {
            var existingModel = GetSingleItem(id);
            if (existingModel == null)
            {
                return NotFound();
            }

            _dbsetGetter(_context).Remove(existingModel);
            _context.SaveChanges();
            return new NoContentResult();
        }

        protected abstract TModel UpdateExistingItem(TModel existingModel, TModel newModel);

        protected TModel GetSingleItem(long id)
        {
            return _dbsetGetter(_context).FirstOrDefault(t => t.Id == id && IsOwnedByCurrentUser(t));
        }

        protected bool IsOwnedByCurrentUser(TModel model)
        {
            return !string.IsNullOrEmpty(model.Creator) &&
                model.Creator.Equals(CurrentUserName());
        }

        protected string CurrentUserName()
        {
            return this.User.Identity.Name;
        }
    }
}