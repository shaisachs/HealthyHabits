using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HealthyHabits.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HealthyHabits.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseModel
    {
        private readonly HealthyHabitsContext _context;
        private readonly Func<HealthyHabitsContext, DbSet<T>> _dbsetGetter;
        private readonly string _getSingularRouteName;

        public BaseController(HealthyHabitsContext context, 
            Func<HealthyHabitsContext, DbSet<T>> dbsetGetter,
            string getSingularRouteName)
        {
            _context = context;
            _dbsetGetter = dbsetGetter;
            _getSingularRouteName = getSingularRouteName;
        }

        protected BaseModelCollection<T> GetAllBase(Func<T, bool> additionalFilter = null)
        {
            Func<T, bool> predicate =
                (t) => IsOwnedByCurrentUser(t) &&
                    (additionalFilter == null ? true : additionalFilter(t));

            var items = _dbsetGetter(_context).Where(predicate).ToList();
            var answer = new BaseModelCollection<T>() { Items = items };

            return answer;
        }

        protected IActionResult GetByIdBase(long id)
        {
            var item = GetSingleItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        protected IActionResult CreateBase(T item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Created = DateTime.Now;
            item.Creator = CurrentUserName();

            _dbsetGetter(_context).Add(item);
            _context.SaveChanges();

            return CreatedAtRoute(_getSingularRouteName, new { id = item.Id }, item);
        }

        protected IActionResult UpdateBase(long id, T newItem)
        {
            if (newItem == null || newItem.Id != id)
            {
                return BadRequest();
            }

            var existingItem = GetSingleItem(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem = UpdateExistingItem(existingItem, newItem);

            _dbsetGetter(_context).Update(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        protected IActionResult DeleteBase(long id)
        {
            var existingItem = GetSingleItem(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _dbsetGetter(_context).Remove(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        protected abstract T UpdateExistingItem(T existingItem, T newItem);

        protected T GetSingleItem(long id)
        {
            return _dbsetGetter(_context).FirstOrDefault(t => t.Id == id && IsOwnedByCurrentUser(t));
        }

        protected bool IsOwnedByCurrentUser(T item)
        {
            return !string.IsNullOrEmpty(item.Creator) &&
                item.Creator.Equals(CurrentUserName());
        }

        protected string CurrentUserName()
        {
            return this.User.Identity.Name;
        }
    }
}