using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IUpdateCategoryUseCase
   {
      void Execute(Category category);
   }
}