using ReminderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IFindCategoryUseCase
   {
      IEnumerable<Category> Execute(Expression<Func<Category, bool>> predicate);
   }
}