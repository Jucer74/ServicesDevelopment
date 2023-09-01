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
            int? doubleCreditCard = StringTransformer.StringToInt(cuttedCreditCard);
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



}