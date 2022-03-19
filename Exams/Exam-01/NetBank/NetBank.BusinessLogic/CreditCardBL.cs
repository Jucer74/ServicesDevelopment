using System;
using System.Linq;
using System.Text;

namespace LuhnAlgorithm.Library
{
    public static class LuhnAlgorithmValidator
    {
        private static int MOD_10 = 010;
        private static int MOD_18 = 018;
        public static bool IsValid(string creditCardNumber)
        {
            var digitsOnly = GetDigits(creditCardNumber);
           

            if (digitsOnly.Length >= MOD_18 || digitsOnly.Length < MOD_10) return false;
           
            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for (var i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digit = int.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                        addend -= 9;
                }
                else
                    addend = digit;

                sum += addend;

                timesTwo = !timesTwo;

            }
            return (sum % MOD_10) == 0;
            return (sum % MOD_18) == 0;

        }
        private static StringBuilder GetDigits(string creditCardNumber)
        {
            var digitsOnly = new StringBuilder();
            foreach (var character in creditCardNumber)
            {
                if (char.IsDigit(character))
                    digitsOnly.Append(character);
            }
            return digitsOnly;
        }
    }
}