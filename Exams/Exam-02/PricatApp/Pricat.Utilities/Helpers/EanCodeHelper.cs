using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Utilities.Helpers
{
    public static class EanCodeHelper
    {
        public static bool IsValidEan13(string eanCode)
        {
            return eanCode.Length == 13 && eanCode.All(char.IsDigit);
        }
    }
}
