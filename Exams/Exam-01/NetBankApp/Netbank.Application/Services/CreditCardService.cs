using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using System;
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
        public async Task<ValidationResultType> GetAll()
    {
        return await  _issuingNetworkRepository.get();
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        IEnumerable<IssuingNetwork> issuingNetworks = await _issuingNetworkRepository.GetAllAsync();

        List<IssuingNetworkData> issuingNetworkDataList = new List<IssuingNetworkData>();

        foreach (IssuingNetwork issuingNetwork in issuingNetworks)
        {
            IssuingNetworkData issuingNetworkData = CreditCardMapper.MapToDto(issuingNetwork); // Realiza el mapeo
            issuingNetworkDataList.Add(issuingNetworkData);
        }

        return issuingNetworkDataList;
    }


    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        IEnumerable<IssuingNetwork> issuingNetwork = await _issuingNetworkRepository.GetAllAsync();

        // Load Data From DataBase
        return issuingNetwork.ToList();
    }

    public IssuingNetworkData GetIssuingNetworkData(IssuingNetwork issuingNetwork)
    {
        return CreditCardMapper.MapToDto(issuingNetwork);
    }
}