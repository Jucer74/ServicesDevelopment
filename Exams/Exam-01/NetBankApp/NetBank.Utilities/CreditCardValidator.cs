using System.Text;
using NetBank.Domain.Dto;

namespace NetBank.Utilities;

public static class CreditCardValidator
{
    private const int MAX_VALUE_DIGIT = 9;
    private const int MIN_LENGTH = 13;
    private const int MAX_LENGTH = 19;

    public static bool IsValid(string creditCardNumber)
    {
        int sum = 0;
        int digit = 0;
        int addend = 0;
        bool timesTwo = false;

        var digitsOnly = GetDigits(creditCardNumber);

        if (digitsOnly.Length > MAX_LENGTH || digitsOnly.Length < MIN_LENGTH) return false;

        for (var i = digitsOnly.Length - 1; i >= 0; i--)
        {
            digit = int.Parse(digitsOnly.ToString(i, 1));
            if (timesTwo)
            {
                addend = digit * 2;
                if (addend > MAX_VALUE_DIGIT)
                    addend -= MAX_VALUE_DIGIT;
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
    
    // Check if credit card number is in range
    public static bool IsInRange(string creditCardNumber, RangeNumber range)
    {   
        // Check if range is valid
        int numberLength = range.MinValue.ToString().Length;
        int initialDigists = int.Parse(creditCardNumber.Substring(0, numberLength));
            
        //  Validate if initial digits are in range
        var result = initialDigists >= range.MinValue && initialDigists <= range.MaxValue;
        return result;
    }

    // Parse range string to RangeNumber
    public static RangeNumber? ParseRange(string? rangeString)
    {   
        // Check if range is valid
        if (string.IsNullOrEmpty(rangeString)) return null;
            
        // Range is in format: 1000-2000
        var range = rangeString.Split('-');
        if (range.Length != 2)
        {
            return null;
        }
            
        // Check if range is valid
        if (!int.TryParse(range[0], out int minValue) || !int.TryParse(range[1], out int maxValue))
        {
            return null;
        }
            
        // Check if range is valid
        return new RangeNumber(minValue, maxValue);
    }
}