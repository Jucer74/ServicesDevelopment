using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arepas.Domain.Entities.Dtos
{
    public class CursorParams
    {
        public int Count { get; set; } = 50;

        public int Cursor { get; set; } = 50;
    }
}
