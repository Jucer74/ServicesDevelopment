using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IAddCategoryUseCase
   {
      int Execute(Category category);
   }
}