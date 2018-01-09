using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HealthyHabits.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HealthyHabits.Dtos;
using HealthyHabits.Translators;
using HealthyHabits.Repositories;

namespace HealthyHabits.Controllers
{
    public abstract class BaseController<TModel, TDto> : Controller
        where TModel : BaseModel
        where TDto : BaseDto<TModel>
    {
        private readonly string _getSingularRouteName;
        private readonly BaseTranslator<TModel, TDto> _translator;
        private readonly BaseRepository<TModel> _repo;

        public BaseController(string getSingularRouteName,
            BaseTranslator<TModel, TDto> translator,
            BaseRepository<TModel> repo)
        {
            _getSingularRouteName = getSingularRouteName;
            _translator = translator;
            _repo = repo;
        }

        protected BaseDtoCollection<TModel, TDto> GetAllBase(Func<TModel, bool> additionalFilter = null)
        {
            var models = _repo.GetAllItems(CurrentUserName(), additionalFilter);
            var dtos = from model in models select _translator.Translate(model);
            var answer = new BaseDtoCollection<TModel, TDto>() { Items = dtos };

            return answer;
        }

        protected IActionResult GetByIdBase(long id)
        {
            var model = _repo.GetSingleItem(id, CurrentUserName());

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

            model = _repo.CreateItem(model, CurrentUserName());

            return CreatedAtRoute(_getSingularRouteName, new { id = model.Id }, _translator.Translate(model));
        }

        protected IActionResult UpdateBase(long id, TDto newDto)
        {
            if (newDto == null || newDto.Id != id)
            {
                return BadRequest();
            }

            var newModel = _translator.Translate(newDto);

            var existingModel = _repo.GetSingleItem(id, CurrentUserName());

            if (existingModel == null)
            {
                return NotFound();
            }

            existingModel = UpdateExistingItem(existingModel, newModel);
            _repo.UpdateItem(existingModel);

            return new NoContentResult();
        }

        protected IActionResult DeleteBase(long id)
        {
            var existingModel = _repo.GetSingleItem(id, CurrentUserName());
            if (existingModel == null)
            {
                return NotFound();
            }

            _repo.DeleteItem(existingModel);
            return new NoContentResult();
        }

        protected abstract TModel UpdateExistingItem(TModel existingModel, TModel newModel);

        protected string CurrentUserName()
        {
            return this.User.Identity.Name;
        }
    }
}