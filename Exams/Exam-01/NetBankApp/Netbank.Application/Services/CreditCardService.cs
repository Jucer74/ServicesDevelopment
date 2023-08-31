using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using System.Linq;

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
        // Check if the number is only digits
        if (!long.TryParse(creditCardNumber, out long creditCardNumberAsLong))
        {
            return ValidationResultType.BadRequest;
        }

        // Check if the number has the correct length
        int creditCardLength = creditCardNumber.Length;
        if (creditCardLength < 13 || creditCardLength > 19)
        {
            return ValidationResultType.BadRequest;
        }

        // Check the Luhn checksum
        int checksum = 0;
        for (int i = creditCardLength - 1; i >= 0; i--)
        {
            int digit = creditCardNumber[i] - '0';

            if ((creditCardLength - i) % 2 == 0)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            checksum += digit;
        }

        if (checksum % 10 != 0)
        {
            return ValidationResultType.BadRequest;
        }

        // Check the issuing network
        List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();
        bool isValidNetwork = false;
        foreach (var issuingNetworkData in issuingNetworkDataList)
        {
            if (creditCardNumber.StartsWith(issuingNetworkData.IssuerPrefix))
            {
                isValidNetwork = true;
                break;
            }
        }

        if (!isValidNetwork)
        {
            return ValidationResultType.NotFound;
        }

        // The card is valid
        return ValidationResultType.Ok;
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        // Convert Data to List Data
        throw new NotImplementedException();
    }

    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        // Load Data From DataBase
        throw new NotImplementedException();

    }
}