using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;
using System.Text.RegularExpressions;

namespace Netbank.Application.Services;

public class CreditCardService : ICreditCardService
{
    #region Loval-Vars

    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

    // Regular Expression To Validate Only Numbers
    private const string NUMBER_REGEX = "^[0-9]*$";

    #endregion Loval-Vars

    #region Properties

    public CreditCardResult Result { get; set; } = null!;

    #endregion Properties

    public CreditCardService(IIssuingNetworkRepository issuingNetworkRepository)
    {
        _issuingNetworkRepository = issuingNetworkRepository;
    }

    public async Task<ValidationResultType> Validate(string creditCardNumber)
    {
        bool isValidCreditCardLength;
        bool isCreditCardNumberValid;
        string issuingNetworkName;
        List<IssuingNetworkData> issuingNetworkDataList;


        // Call the Individual Validations

        //Checks if there is any letter in the credit card number
        if (!Regex.IsMatch(creditCardNumber, NUMBER_REGEX))
        {
            string requestMessage = ErrorMessageFormatter.AddSpaceBetweenLowerAndCapitalLetter(ValidationResultType.BadRequest.ToString());

            Result = new CreditCardResult(requestMessage, false);

            return ValidationResultType.BadRequest;
        }

        issuingNetworkDataList = await LoadIssuingNetworkDataAsync();

        issuingNetworkName = CreditCardValidator.FindIssuingNetwork(creditCardNumber, issuingNetworkDataList);

        //Checks if the issuing network was not found
        if (string.IsNullOrEmpty(issuingNetworkName))
        {
            string requestMessage = ErrorMessageFormatter.AddSpaceBetweenLowerAndCapitalLetter(ValidationResultType.NotFound.ToString());

            Result = new CreditCardResult(requestMessage, false);
            return ValidationResultType.NotFound;
        }


        isValidCreditCardLength = CreditCardValidator.IsValidCreditCardLength(creditCardNumber, issuingNetworkDataList, issuingNetworkName);
        isCreditCardNumberValid = CreditCardValidator.IsValid(creditCardNumber);

        if (isValidCreditCardLength && isCreditCardNumberValid)//As the issuing network was found, it checks if the credit number is valid and if it has the appropriate length
        {
            Result = new CreditCardResult(issuingNetworkName, true);
            return ValidationResultType.Ok;
        }

        Result = new CreditCardResult(issuingNetworkName, false); //The issuing network was found, but the credit card number is invalid or it does not have the appropiate length
        return ValidationResultType.Ok;
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkDataAsync()
    {
        // Convert Data to List Data
        List<IssuingNetwork> issuingNetworkList = await this.GetIssuingNetworksAsync();

        List<IssuingNetworkData> issuingNetworkDataList = new();

        foreach (IssuingNetwork issuingNetwork in issuingNetworkList)
        {
            IssuingNetworkData issuingNetworkData = new()
            {
                Name = issuingNetwork.Name,
                StartsWithNumbers = ConvertNumbersToList(issuingNetwork.StartsWithNumbers),
                InRange = CreateRangeNumberObject(issuingNetwork.InRange),
                AllowedLengths = ConvertNumbersToList(issuingNetwork.AllowedLengths)
            };

            issuingNetworkDataList.Add(issuingNetworkData);
        }

        return issuingNetworkDataList;
    }

    private async Task<List<IssuingNetwork>> GetIssuingNetworksAsync()
    {
        // Load Data From DataBase
        IEnumerable<IssuingNetwork> issuingNetwork = await _issuingNetworkRepository.GetAllAsync();

        return issuingNetwork.ToList();
    }

    private static List<int> ConvertNumbersToList(string? numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return new List<int>();
        }

        //Creates a list of strings using the Split method
        List<string> numbersStringList = numbers.Split(",").Select(num => num.Trim()).ToList();

        //Parses every string of the list to an integer
        List<int> numbersIntList = numbersStringList.Select(int.Parse).ToList();

        return numbersIntList;
    }

    private static RangeNumber? CreateRangeNumberObject(string? inRange)
    {
        RangeNumber rangeNumber = new();

        if (string.IsNullOrEmpty(inRange))
        {
            return null;
        }

        //Separates the minimun and maximun number by the '-' char
        string[] rangeParts = inRange.Trim().Split('-');

        rangeNumber.MinValue = int.Parse(rangeParts[0]);
        rangeNumber.MaxValue = int.Parse(rangeParts[1]);

        return rangeNumber;

    }
}