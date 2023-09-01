using NetBank.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netbank.Application.Utils
{
    public class StringTransformer
    {
        public static List<int>? ComaSeparatedValuesToIntList(string csv)
        {
            List<int>? intList = null;
            if(csv != null) {
                intList = new List<int>(); 
                List<string> stringList = SeparatedValuesToStringList(csv, ',');
                foreach (var str in stringList)
                {
                    intList.Add(StringToInt(str));
                }
            }
            return intList;
        }

        public static RangeNumber? HyphenSeparatedValuesToRangeNumber(string hsv) {
            RangeNumber? rangeNumber = null;
            if(hsv != null)
            {
                List<int> intList = HyphenSeparatedValuesToIntList(hsv);
                if (intList.Count == 2)
                {
                    rangeNumber = new RangeNumber();
                    rangeNumber.MinValue = intList[0];
                    rangeNumber.MaxValue = intList[1];
                }
            }
            return rangeNumber;
        }

        public static List<int> HyphenSeparatedValuesToIntList(string hsv)
        {
            List<int> intList = new();
            List<string> stringList = SeparatedValuesToStringList(hsv, '-');
            foreach (var str in stringList)
            {
                intList.Add(StringToInt(str));
            }
            return intList;
        }

        public static List<string> SeparatedValuesToStringList(string sv, char separator)
        {
            List<string> stringList = sv.Split(new char[] { separator }).ToList();
            return stringList;
        }

        public static int StringToInt(string str)
        {
            return int.Parse(str);
        }

    }
}
