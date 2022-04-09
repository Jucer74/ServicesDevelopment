using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Domain.interfaces
{
    public interface IAddCategoryUseCase
    {
      int Execute(IAddCategoryUseCase category)
    }
}
