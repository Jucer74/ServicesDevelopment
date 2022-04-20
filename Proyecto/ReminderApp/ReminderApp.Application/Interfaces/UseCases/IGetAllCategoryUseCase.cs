using ReminderApp.Domain.Entities;
using System.Collections.Generic;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IGetAllCategoryUseCase
   {
      IEnumerable<Category> Execute();
   }
}