using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IRemoveCategoryUseCase
   {
      void Execute(Category category);
   }
}