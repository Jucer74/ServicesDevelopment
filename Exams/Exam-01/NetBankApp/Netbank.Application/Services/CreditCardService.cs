using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Exceptions;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Netbank.Application.Services;

public class CreditCardService : ICreditCardService
{
    #region Loval-Vars
    private readonly IIssuingNetworkRepository _issuingNetworkRepository;

    // Regular Expression To Validate Only Numbers
    private const string NUMBER_REGEX = "^[0-9]*$";
    private List<IssuingNetworkData> IssuingNetworkDataList = LoadIssuingNetworkData();

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
        if (!isNumer(creditCardNumber)) {
            Result = BadRequestResult();
        }

        string issuingNetwork = GetIssuingNetworks(creditCardNumber);

        if (issuingNetwork is null)
        {
            Result= BadRequestResult();
            return ValidationResultType.NotFound;
        }
        Result = new CreditCardResult(issuingNetwork, true);
        return ValidationResultType.Ok;


        //throw new NotImplementedException();
    }


    //*****************************************************DETERMINAR TIPO TARJETA
    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        // Convert Data to List Data
        StreamReader r = new StreamReader("IssuingNetworkData.json");
        string jsonString = r.ReadToEnd();
        return JsonSerializer.Deserialize<List<IssuingNetworkData>>(jsonString);
        //throw new NotImplementedException();
    }

     

    private async Task<List<IssuingNetwork>> GetIssuingNetworks(string creditCardNumber)
    {
        string Name = null;
        foreach(var issuingNetworkItem  in IssuingNetworkDataList)
        {
            if(isValidStartsWithNumbers(issuingNetworkItem.StartsWithNumbers, creditCardNumber))
            {
                return issuingNetworkItem.Name;
            }
            if (isValidRange(issuingNetworkItem.InRange, creditCardNumber){
                return issuingNetworkItem.Name;
            }
        }

        return Name;

        //throw new NotImplementedException();
    }





    private bool isValidStartsWithNumbers(List<int>? startsWithNumbers, string creditCardNumber)
    {
        throw new NotImplementedException();
    }

    private bool isValidRange(RangeNumber? inRange, string creditCardNumber)
    {
        throw new NotImplementedException();
    }






    private bool isNumer(string creditCardNumber){
        if (string.IsNullOrEmpty(creditCardNumber))
        {
            return false;
        }
        return new Regex(NUMBER_REGEX).IsMatch(creditCardNumber);
    }











    private static CreditCardResult BadRequestResult()
    {
        return new CreditCardResult("Bad Request", false);
    }

    private static CreditCardResult NotFoundResult()
    {
        return new CreditCardResult("Not found", false);
    }

    
    


}