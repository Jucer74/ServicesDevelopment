﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Common
{
    public interface IRepository<T> where T : EntitiyBase
    {
        T GetValue(int id);
    }
}
