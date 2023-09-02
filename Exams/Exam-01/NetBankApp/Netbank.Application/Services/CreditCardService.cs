using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static NetBank.Utilities.CreditCardValidator;

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
        // Carga los datos de la red emisora
        var issuingNetworkDataList = await LoadIssuingNetworkData();

        if (!IsValidCreditCardNumber(creditCardNumber))
        {
            Result = new CreditCardResult("Bad Request", false);
            return ValidationResultType.BadRequest;
        }

        var creditCardLength = creditCardNumber.Length;

        IssuingNetworkData? foundNetwork = null;

        // Encuentra la red emisora
        string issuingNetworkName = FindIssuingNetwork(creditCardNumber, issuingNetworkDataList);

        // Verifica si issuingNetworkName está vacío o nulo
        if (string.IsNullOrEmpty(issuingNetworkName))
        {
            string requestMessage = ErrorMessageFormatter.AddSpaceBetweenLowerAndCapitalLetter(ValidationResultType.NotFound.ToString());

            Result = new CreditCardResult(requestMessage, false);
            return ValidationResultType.NotFound;
        }

        foreach (var network in issuingNetworkDataList)
        {
            var networkMatches = NetworkMatches(network, creditCardNumber, creditCardLength);

            if (networkMatches == ValidationResultType.Ok)
            {
                foundNetwork = network;
                break;
            }
            else if (networkMatches == ValidationResultType.BadRequest)
            {
                Result = new CreditCardResult("Bad Request", false);
                return ValidationResultType.BadRequest;
            }
        }

        if (foundNetwork != null)
        {
            // Verifica la longitud permitida antes de realizar la validación de Luhn
            bool isValidCreditCardLength = IsValidCreditCardLength(creditCardNumber, issuingNetworkDataList, foundNetwork.Name);
            bool isCreditCardNumberValid = CreditCardValidator.IsValid(creditCardNumber);

            if (isValidCreditCardLength && isCreditCardNumberValid)
            {
                // La tarjeta de crédito es válida según las reglas de la red emisora y el algoritmo de Luhn.
                Result = new CreditCardResult(foundNetwork.Name, true);
                return ValidationResultType.Ok;
            }
        }

        // Si no se encontró una red emisora válida o la tarjeta no es válida según las reglas de la red emisora y Luhn,
        // establece el resultado en "Not Found" si no se encuentra una red emisora válida, y luego configuramos el resultado en "false" en cualquier otro caso.
        Result = new CreditCardResult(issuingNetworkName ?? "Not Found", false);
        return ValidationResultType.Ok;
    }

    public static string FindIssuingNetwork(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList)
    {
        string issuingNetworkName = "";
        bool startsWithNumbers = false;
        bool startsWithNumbersInRange = false;

        foreach (IssuingNetworkData issuingNetworkData in issuingNetworkDataList)
        {
            if (issuingNetworkData.StartsWithNumbers?.Any() ?? false)
            {
                startsWithNumbers = StartsWithNumberFromList(creditCardNumber, issuingNetworkData.StartsWithNumbers);
            }

            if (issuingNetworkData.InRange != null)
            {
                startsWithNumbersInRange = StartsWithNumberInRange(creditCardNumber, issuingNetworkData.InRange);
            }

            if (startsWithNumbers || startsWithNumbersInRange)
            {
                issuingNetworkName = issuingNetworkData.Name;
                break;
            }
        }

        return issuingNetworkName;
    }

    private static bool StartsWithNumberInRange(string creditCardNumber, RangeNumber rangeNumber)
    {
        int numberLength = rangeNumber.MinValue.ToString().Length;

        if (creditCardNumber.Length < numberLength) { return false; }//If the length of the credit card number is shorter than the minimum allowable length, then the credit card number is out of range

        int initalCreditCardDigits = int.Parse(creditCardNumber[..numberLength]);//Takes the initial digits of the credit card 

        //Checks if the initial digits of the credit card number are between a range
        return (initalCreditCardDigits >= rangeNumber.MinValue) && (initalCreditCardDigits <= rangeNumber.MaxValue);
    }

    private static bool StartsWithNumberFromList(string creditCardNumber, List<int> numberList)
    {
        //Checks if the credit card number starts with any number inside a particular list of numbers
        int numberLength;
        int initalCreditCardDigits;

        foreach (int number in numberList)
        {
            numberLength = number.ToString().Length;

            if (creditCardNumber.Length < numberLength) { continue; } //If the length of the credit card number is shorter than expected, those numbers will never be equal

            initalCreditCardDigits = int.Parse(creditCardNumber[..numberLength]);//Takes the initial digits of the credit card 

            if (initalCreditCardDigits == number)//Compares the initial digits of the credit card with each element of the list
            {
                return true;
            }
        }

        return false;
    }

    private ValidationResultType NetworkMatches(IssuingNetworkData network, string creditCardNumber, int creditCardLength)
    {

        // Verifica si el número de tarjeta de crédito es válido.
        if (!IsValidCreditCardNumber(creditCardNumber))
        {
            return ValidationResultType.BadRequest;
        }

        // Busca el prefijo coincidente más largo en los prefijos de la red emisora.
        var matchingPrefix = FindMatchingPrefix(network, creditCardNumber);

        if (!string.IsNullOrEmpty(matchingPrefix))
        {
            // Se encontró un prefijo coincidente. Ahora verifica la longitud permitida.
            if (network.AllowedLengths != null && network.AllowedLengths.Contains(creditCardLength))
            {
                // La longitud de la tarjeta de crédito es válida según las reglas de la red emisora.
                return ValidationResultType.Ok;
            }
            else
            {
                return ValidationResultType.NotFound;
            }
        }

        // Si InRange no es null, verifica si el número de tarjeta de crédito está en el rango especificado.
        if (network.InRange != null)
        {
            var range = network.InRange;
            int cardNumberPrefix;

            if (int.TryParse(creditCardNumber.Substring(0, range.MinValue.ToString().Length), out cardNumberPrefix))
            {
                if (cardNumberPrefix >= range.MinValue && cardNumberPrefix <= range.MaxValue)
                {
                    // El prefijo del número de tarjeta de crédito está dentro del rango permitido según las reglas de la red emisora.

                    // Ahora verifica la longitud permitida.
                    if (network.AllowedLengths != null && network.AllowedLengths.Contains(creditCardLength))
                    {
                        // La longitud de la tarjeta de crédito es válida según las reglas de la red emisora.
                        return ValidationResultType.Ok;
                    }
                    else
                    {
                        return ValidationResultType.NotFound;
                    }
                }
                else
                {
                    return ValidationResultType.NotFound;
                }
            }
        }

        // Si el código llega aquí, significa que no se encontró un prefijo coincidente ni se aplicó el rango permitido.
        return ValidationResultType.NotFound;
    }

    public static bool IsValidCreditCardLength(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList, string issuingNetworkName)
    {
        IssuingNetworkData issuingNetworkData = issuingNetworkDataList.Single(issuingNetworkData => issuingNetworkData.Name.Trim() == issuingNetworkName.Trim());//Finds the data of the issuing network
        int creditCardNumberLength = creditCardNumber.Trim().Length;

        return issuingNetworkData.AllowedLengths.Contains(creditCardNumberLength);
    }

    private string FindMatchingPrefix(IssuingNetworkData network, string creditCardNumber)
    {
        if (network.StartsWithNumbers != null && network.StartsWithNumbers.Any())
        {
            // Ordena los prefijos de manera descendente para verificar el prefijo más largo primero.
            var orderedPrefixes = network.StartsWithNumbers.OrderByDescending(p => p);

            foreach (var prefix in orderedPrefixes)
            {
                var prefixString = prefix.ToString();
                if (creditCardNumber.StartsWith(prefixString))
                {
                    // Se encontró un prefijo coincidente.
                    return prefixString;
                }
            }
        }

        return string.Empty; // No se encontraron prefijos coincidentes.
    }



    private static bool IsValidCreditCardNumber(string creditCardNumber)
    {
        return Regex.IsMatch(creditCardNumber, NUMBER_REGEX);
    }

    private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
    {
        List<IssuingNetwork> issuingNetworks = await GetIssuingNetworks();
        List<IssuingNetworkData> issuingNetworkDataList = new List<IssuingNetworkData>();

        foreach (IssuingNetwork issuingNetwork in issuingNetworks)
        {
            int minValue = 0;
            int maxValue = 0;

            if (issuingNetwork.InRange != null)
            {
                string[] rangeParts = issuingNetwork.InRange.Split('-');

                if (rangeParts.Length == 2)
                {
                    int.TryParse(rangeParts[0], out minValue);
                    int.TryParse(rangeParts[1], out maxValue);
                }
            }

            IssuingNetworkData networkData = new IssuingNetworkData
            {
                Name = issuingNetwork.Name,
                StartsWithNumbers = issuingNetwork.StartsWithNumbers?.Split(',').Select(int.Parse).ToList(),
                InRange = issuingNetwork.InRange != null ? new RangeNumber
                {
                    MinValue = minValue,
                    MaxValue = maxValue
                } : null,
                AllowedLengths = issuingNetwork.AllowedLengths.Split(',').Select(int.Parse).ToList()
            };

            issuingNetworkDataList.Add(networkData);
        }
         
        return issuingNetworkDataList;
    }
    private async Task<List<IssuingNetwork>> GetIssuingNetworks()
    {
        var issuingNetwork = await _issuingNetworkRepository.GetAllAsync();
        return issuingNetwork.ToList();
    }
}