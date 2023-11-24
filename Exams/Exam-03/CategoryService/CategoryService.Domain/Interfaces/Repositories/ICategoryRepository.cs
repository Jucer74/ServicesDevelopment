using CategoryService.Domain.Common;
using CategoryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
