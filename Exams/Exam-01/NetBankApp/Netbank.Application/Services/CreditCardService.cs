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
        // List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();

        // Call the Individual Validations

        throw new NotImplementedException();
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
    public IssuingNetworkData GetIssuingNetworkData(IssuingNetwork issuingNetwork)
    {
        return CreditCardMapper.MapToDto(issuingNetwork);
    }
    public string IdentifyIssuingNetwork(string creditCardNumber)
    {
        var issuingNetworks = _issuingNetworkRepository.GetAllIssuingNetworks();

        foreach (var network in issuingNetworks)
        {
            // Comprobar si el número de tarjeta comienza con alguno de los números especificados para esta red emisora
            var startsWithNumbers = network.StartsWithNumbers.Split(',').ToList();
            if (startsWithNumbers.Any(num => creditCardNumber.StartsWith(num)))
            {
                return network.Name;
            }

            // Comprobar si el número de tarjeta está dentro del rango especificado para esta red emisora
            if (!string.IsNullOrEmpty(network.InRange))
            {
                var range = network.InRange.Split('-');
                var minValue = int.Parse(range[0]);
                var maxValue = int.Parse(range[1]);

                var cardNumberInt = int.Parse(creditCardNumber.Substring(0, range[0].Length)); // Tomar la misma cantidad de dígitos que el rango
                if (cardNumberInt >= minValue && cardNumberInt <= maxValue)
                {
                    return network.Name;
                }
            }
        }

        return "Unknown";
    }

}