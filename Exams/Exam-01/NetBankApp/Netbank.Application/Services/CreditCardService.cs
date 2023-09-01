using Netbank.Application.Interfaces;
using NetBank.Application.Mapping;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
namespace Netbank.Application.Services;
using NetBank.Domain;
using NetBank.Utilities;

public class CreditCardService : ICreditCardService
{
    #region Loval-Vars

    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

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
        ValidationResultType validationResultType;
        Boolean isValidCreditCard = false;
        string? foundIssuingNetworkDataName;
        List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();
        if (!NumberConverter.StringToDouble(creditCardNumber).HasValue)
        {
            validationResultType = ValidationResultType.BadRequest;
            foundIssuingNetworkDataName = "Bad Request";
        }
        else
        {
            // Call the Individual Validations
            isValidCreditCard = CreditCardValidator.IsValid(creditCardNumber);
            foundIssuingNetworkDataName = FindIssuingNetworkOwnerName(issuingNetworkDataList, creditCardNumber);
            if (foundIssuingNetworkDataName != null)
            {
                validationResultType = ValidationResultType.Ok;
            }
            else
            {
                validationResultType = ValidationResultType.NotFound;
                foundIssuingNetworkDataName = "Not Found";
            }
        }
        this.Result = new CreditCardResult(foundIssuingNetworkDataName, isValidCreditCard);
        return validationResultType;
    }

    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        // Load Data From DataBase
        IEnumerable<IssuingNetwork> issuingNetworks = await this._issuingNetworkRepository.GetAllAsync();
        return issuingNetworks.ToList();
    }

    private static string? FindIssuingNetworkOwnerName(List<IssuingNetworkData> issuingNetworkDataList, string creditCardNumber)
    {
        string? foundIssuingNetworkDataName = null;
        foreach (IssuingNetworkData issuingNetworkData in issuingNetworkDataList)
        {
            if (issuingNetworkData.ValidateCreditCard(creditCardNumber))
            {
                foundIssuingNetworkDataName ??= issuingNetworkData.Name;
                break;
            }
        }
        return foundIssuingNetworkDataName;
    }

    #region 

    public async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        List<IssuingNetwork> issuingNetworks = await this.GetIssuingNetworks();
        List<IssuingNetworkData> issuingNetworkDataList = IssuingNetworkMapping.ToIssuingNetworkDataList(issuingNetworks);
        // Convert Data to List Data
        return issuingNetworkDataList;
    }

    #endregion 
}
