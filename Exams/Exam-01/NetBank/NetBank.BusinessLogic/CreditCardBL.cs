using System;
using System.Text;

namespace NetBank.BussisnesLogic
{
    public class CreditCardBL
    {
        public static class LuhnAlgorithmValidator
        {
            private const int V = 18;
            private const int V2 = 15;
            private const int V3 = 2;
            private const int V4 = 9;
            public static bool IsValid(string creditCardNumber)
            {
                var digitsOnly = GetDigits(creditCardNumber);
                if (digitsOnly.Length > V || digitsOnly.Length < V2) return false;

                int sum = 0;
                int digit = 0;
                int addend = 0;
                bool timesTwo = false;

                for (var i = digitsOnly.Length - 1; i >= 0; i--)
                {
                    digit = int.Parse(digitsOnly.ToString(i, 1));
                    if (timesTwo)
                    {
                        addend = digit * V3;
                        if (addend > V4)
                            addend -= V4;
                    }
                    else
                        addend = digit;

                    sum += addend;

                    timesTwo = !timesTwo;

                }
                return (sum % 10) == 0;
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
}