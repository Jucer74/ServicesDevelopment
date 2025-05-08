using System.Linq;

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