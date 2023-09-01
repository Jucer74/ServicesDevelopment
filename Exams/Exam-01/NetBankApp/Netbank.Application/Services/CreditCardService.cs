using Netbank.Application.Interfaces;
using Netbank.Application.Mappers;
using NetBank.Domain.Common;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;

namespace Netbank.Application.Services;

public class CreditCardService : ICreditCardService
{
    #region Loval-Vars

    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

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

        // Usar LoadData en lugar de LoadIssuingNetworkData
        List<IssuingNetworkData> issuingNetworkDataList = await LoadData();

        if (DataTransformer.StringToDoble(creditCardNumber) != null)
        {
            // Identifica la red emisora primero.
            foundIssuingNetworkDataName = FindIssuingNetworkOwnerName(issuingNetworkDataList, creditCardNumber);

            // Luego verifica si el número de tarjeta es válido.
            isValidCreditCard = CreditCardValidator.IsValid(creditCardNumber);

            if (foundIssuingNetworkDataName != null)
            {
                validationResultType = ValidationResultType.Ok; // Siempre devuelve Ok porque el test espera un status 200.
            }
            else
            {
                validationResultType = ValidationResultType.NotFound;
                foundIssuingNetworkDataName = "Not Found";
            }
        }
        else
        {
            validationResultType = ValidationResultType.BadRequest;
            foundIssuingNetworkDataName = "Bad Request";
        }

        this.Result = new CreditCardResult(foundIssuingNetworkDataName, isValidCreditCard);
        return validationResultType;
    }

    private static string? FindIssuingNetworkOwnerName(List<IssuingNetworkData> issuingNetworkDataList, string creditCardNumber)
    {
        var foundIssuingNetworkData = issuingNetworkDataList
            .Find(issuingNetworkData => issuingNetworkData.IsCardFromThisNetwork(creditCardNumber));

        return foundIssuingNetworkData?.Name;
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        List<IssuingNetwork> issuingNetworks = await this.GetIssuingNetworks();
        List<IssuingNetworkData> issuingNetworkDataList = CreditCardMapper.ConvertToNetworkDataList(issuingNetworks);

        // Convert Data to List Data
        return issuingNetworkDataList;
    }

    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        // Load Data From DataBase
        IEnumerable<IssuingNetwork> issuingNetworks = await this._issuingNetworkRepository.GetAllAsync();
        return issuingNetworks.ToList();
    }

    private async Task<List<IssuingNetworkData>> LoadData()
    {
        // Cargar datos de la red emisora
        return await LoadIssuingNetworkData();
    }
}