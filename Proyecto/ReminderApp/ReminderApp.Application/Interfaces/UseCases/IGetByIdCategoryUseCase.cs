using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Interfaces.Usecases
{
   public interface IGetByIdCategoryUseCase
   {
      Category Execute(int id);
   }
}