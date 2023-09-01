using System;
using System.Collections.Generic;
using System.Linq;

namespace NetBank.Domain
{
    using Dto;

    public class NumberConverter
    {
        public static List<int>? ComaSeparatedValuesToIntList(string csv)
        {
            if (csv == null)
            {
                return null;
            }

            var intList = new List<int>();
            var stringList = SeparatedValuesToStringList(csv, ',');

            foreach (var str in stringList)
            {
                var intValue = StringToInt(str);
                if (intValue.HasValue)
                {
                    intList.Add(intValue.Value);
                }
            }

            return intList;
        }

        public static RangeNumber? HyphenSeparatedValuesToRangeNumber(string hsv)
        {
            if (hsv == null)
            {
                return null;
            }

            var intList = HyphenSeparatedValuesToIntList(hsv);

            if (intList.Count == 2)
            {
                var rangeNumber = new RangeNumber
                {
                    MinValue = intList[0],
                    MaxValue = intList[1]
                };
                return rangeNumber;
            }

            return null;
        }

        public static List<int> HyphenSeparatedValuesToIntList(string hsv)
        {
            var intList = new List<int>();
            var stringList = SeparatedValuesToStringList(hsv, '-');

            foreach (var str in stringList)
            {
                var num = StringToInt(str);
                if (num.HasValue)
                {
                    intList.Add(num.Value);
                }
            }

            return intList;
        }

        public static List<string> SeparatedValuesToStringList(string sv, char separator)
        {
            return sv.Split(new char[] { separator }).ToList();
        }

        public static int? StringToInt(string str)
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

        public static double? StringToDouble(string str)
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
