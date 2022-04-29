using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application.exception
{
    public class Exceptions:Exception
    {
        public Exceptions()
        {

        }
        public Exceptions(string msj):base(msj)
        {

        }
    }
}
