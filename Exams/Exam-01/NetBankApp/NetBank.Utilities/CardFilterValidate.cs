using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetBank.Utilities
{
    public static class CardFilterValidate
    {

        public static bool SearchIssuingNetwork(string cardNumber, IssuingNetworkData issuingNetwork)
        {
            if (issuingNetwork.InRange != null && IsNumberInRange(cardNumber, issuingNetwork.InRange))
            {
                return true;
            }

            if (issuingNetwork.StartsWithNumbers != null && StartsWithNumber(cardNumber, issuingNetwork.StartsWithNumbers))
            {
                return true;
            }

            return false;
        }

        public static bool StartsWithNumber(string cardNumber, List<int> startingNumbers)
        {
            

            if (startingNumbers != null )
            {
                
                foreach (int startingNumber in startingNumbers)
                {
                    if (cardNumber.Length >= startingNumber.ToString().Length &&  cardNumber.StartsWith(startingNumber.ToString().Trim()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsNumberInRange(string creditCardNumber, RangeNumber range)
        {
            if (range != null)
            {
                //string[] rangeSplit = range.Split('-');
                int lowerLimit = range.MinValue;
                int upperLimit = range.MaxValue;
                string rangeSplit = lowerLimit.ToString();
                int cardNumberLength = rangeSplit.Length;

                if (creditCardNumber.Length >= rangeSplit.Length){
                    long cardNumberPrefix = long.Parse(creditCardNumber.Substring(0, cardNumberLength));

                    if (cardNumberPrefix >= lowerLimit && cardNumberPrefix <= upperLimit)
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }

        public static bool VerifyLength(string cardNumber, List<int> allowedLengths)
        {
           
            foreach (int length in allowedLengths)
            {
                if (cardNumber.Length == length)
                {
                    return true;
                }
            }
            return false;
        }

        
    }


}
