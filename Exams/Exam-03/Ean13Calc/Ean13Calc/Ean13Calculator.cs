using System;
using System.Text;

namespace Ean13Calc
{
   public static class Ean13Calculator
   {
      private const int EAN_LENGTH = 13;
      private const int WEIGHT_EVEN = 1;
      private const int WEIGHT_ODD = 3;
      private const int BASE_10 = 10;

      public static string GenerateEan13Number()
      {
         Random randomDigits = new Random();

         int baseEanLength = EAN_LENGTH - 1;

         StringBuilder sbBaseEanNumber = new StringBuilder();

         int digit = 0;

         for (int idx = 0; idx < baseEanLength; idx++)
         {
            digit = randomDigits.Next(10);
            sbBaseEanNumber.Append(digit.ToString());
         }

         string bseEanNumber = sbBaseEanNumber.ToString();

         return string.Format("{0}{1}", bseEanNumber, GetCheckDigit(bseEanNumber));
      }

      public static string GenerateEan13Number(string countryCode, string manufacterCode)
      {
         Random randomDigits = new Random();

         StringBuilder sbBaseEanNumber = new StringBuilder();

         sbBaseEanNumber.Append(countryCode);
         sbBaseEanNumber.Append(manufacterCode);

         int baseEanLength = EAN_LENGTH - 1 - sbBaseEanNumber.ToString().Length;

         int digit = 0;

         for (int idx = 0; idx < baseEanLength; idx++)
         {
            digit = randomDigits.Next(10);
            sbBaseEanNumber.Append(digit.ToString());
         }

         string bseEanNumber = sbBaseEanNumber.ToString();

         return string.Format("{0}{1}", bseEanNumber, GetCheckDigit(bseEanNumber));
      }

      public static string GetCheckDigit(string baseEanNumber)
      {
         int baseEanLength = baseEanNumber.Length;
         int digit;
         int partialSum = 0;

         for (int index = 0; index < baseEanLength; index++)
         {
            digit = int.Parse(baseEanNumber[index].ToString());

            partialSum += digit * GetWeight(index);
         }

         int checkDigit = BASE_10 - (partialSum % BASE_10);

         if (checkDigit == BASE_10)
            checkDigit = 0;

         return checkDigit.ToString();
      }

      public static bool IsValid(string eanNumber)
      {
         if (!IsValidLength(eanNumber, EAN_LENGTH) || !IsNumeric(eanNumber))
            return false;

         string bseEanNumber = eanNumber.Substring(0, EAN_LENGTH - 1);

         return eanNumber.Equals(string.Format("{0}{1}", bseEanNumber, GetCheckDigit(bseEanNumber)));
      }

      private static bool IsValidLength(string eanNumber, int allowedLength)
      {
         return eanNumber.Length == allowedLength;
      }

      private static bool IsNumeric(string eanNumber)
      {
         return long.TryParse(eanNumber, out long _);
      }

      private static bool IsEven(int number)
      {
         return number % 2 == 0;
      }

      private static int GetWeight(int index)
      {
         if (IsEven(index)) return WEIGHT_EVEN;

         return WEIGHT_ODD;
      }
   }
}