using NetBank.Domain;
using NetBank.Domain.Define;
using System;

namespace NetBank.Domain.Dto;

public class IssuingNetworkData
{
    public string Name { get; set; } = null!;
    public List<int>? StartsWithNumbers { get; set; } = null!;
    public RangeNumber? InRange { get; set; } = null!;
    public List<int> AllowedLengths { get; set; } = null!;

    public Boolean ValidateCreditCard(string creditCardNumber)
    {
        Boolean isIdentified = false;

        if (this.ValidateStartsNumbers(creditCardNumber) || this.ValidateInRange(creditCardNumber))
        {
            isIdentified = true;
        }

        return isIdentified;
    }

    private Boolean ValidateAllowedLengths(string creditCardNumber)
    {
        Boolean isValid = false;
        if(this.AllowedLengths.Contains(creditCardNumber.Length))
        {
            isValid = true;
        }
        return isValid;


    }

    private Boolean ValidateInRange(string creditCardNumber)
    {
        Boolean isValid = false;
        if (this.InRange != null)
        {
            string numString = this.InRange.MinValue.ToString();
            int numLength = numString.Length;
            string cuttedCreditCard = creditCardNumber.Substring(0, numLength);
            int? doubleCreditCard = Convertidor.ConvertStringToInt(cuttedCreditCard);
            if (doubleCreditCard >= this.InRange.MinValue && doubleCreditCard <= this.InRange.MaxValue)
            {
                isValid = true;
            }
        }
        return isValid;
    }

    private Boolean ValidateStartsNumbers(string creditCardNumber)
    {
        Boolean isValid = false;
        if (this.StartsWithNumbers != null)
        {
            foreach (int num in this.StartsWithNumbers)
            {
                string numString = num.ToString();
                int numLength = numString.Length;
                string cuttedCreditCard = creditCardNumber.Substring(0, numLength);
                if (cuttedCreditCard == numString)
                {
                    isValid = true;
                    break;
                }
            }
        }
        return isValid;
    }

    public class Convertidor
    {
        public static List<int>? ConvertCommaSeparatedToIntList(string coma)
        {
            List<int>? intList = null;
            if (coma != null)
            {
                intList = new List<int>();
                List<string> stringList = ConvertSeparatedValuesToStringList(coma, ',');
                foreach (var str in stringList)
                {
                    int? intValue = ConvertStringToInt(str);
                    if (intValue != null)
                    {
                        intList.Add(intValue.Value);
                    }
                }
            }
            return intList;
        }

        public static RangeNumber? GuionRangeConverter(string guion)
        {
            RangeNumber? rangeNumber = null;
            if (guion != null)
            {
                List<int> intList = ConvertGuionToSeparatedToNumberRange(guion);

                if (intList.Count == 2)
                {
                    rangeNumber = new RangeNumber();
                    rangeNumber.MinValue = intList[0];
                    rangeNumber.MaxValue = intList[1];
                }
            }
            return rangeNumber;
        }

        public static List<int> ConvertGuionToSeparatedToNumberRange(string guion)
        {
            List<int> intList = new();
            List<string> stringList = ConvertSeparatedValuesToStringList(guion, '-');
            foreach (var str in stringList)
            {
                int? num = ConvertStringToInt(str);
                if (num != null)
                {
                    intList.Add(num.Value);
                }
            }
            return intList;
        }

        public static List<string> ConvertSeparatedValuesToStringList(string sv, char separator)
        {
            List<string> stringList = sv.Split(new char[] { separator }).ToList();
            return stringList;
        }

        public static int? ConvertStringToInt(string str)
        {
            int? num;
            try
            {
                num = int.Parse(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                num = null;
            }
            return num;
        }

        public static double? ConvertStringToDouble(string str)
        {
            double? num;
            try
            {
                num = double.Parse(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                num = null;
            }
            return num;
        }
    }

}