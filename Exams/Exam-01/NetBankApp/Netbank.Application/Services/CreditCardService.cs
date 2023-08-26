﻿using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using Pricat.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Netbank.Application.Services;

public class CreditCardService: ICreditCardService
{
    #region Loval-Vars

    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

    private const string NUMBER_REGEX = "^[0-9]*$";

    // List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();

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

        throw new NotImplementedException();
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        throw new NotImplementedException();
    }

    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        throw new NotImplementedException();
    }
}