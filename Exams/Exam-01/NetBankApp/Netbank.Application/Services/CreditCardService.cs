using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;
using System.Text;
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
        // Carga los datos de la red emisora
        var issuingNetworkDataList = await LoadIssuingNetworkData();

        if (!IsValidCreditCardNumber(creditCardNumber))
        {
            Result = new CreditCardResult("Bad Request", false);
            return ValidationResultType.BadRequest;
        }

        _ = creditCardNumber.Length;

        IssuingNetworkData? foundNetwork = null;

        // Encuentra la red emisora
        string issuingNetworkName = FindIssuingNetwork(creditCardNumber, issuingNetworkDataList);

        // Verifica si issuingNetworkName está vacío o nulo
        if (string.IsNullOrEmpty(issuingNetworkName))
        {
            string validationResultType = ValidationResultType.NotFound.ToString();
            string requestMessage = AddSpacesBetweenLowerAndCapitalLetters(validationResultType);

            Result = new CreditCardResult(requestMessage, false);
            return ValidationResultType.NotFound;
        }

        foreach (var network in issuingNetworkDataList)
        {
            var networkMatches = NetworkMatches(network, creditCardNumber); // Elimina creditCardLength de aquí

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

        // Si no se encontró una red emisora válida o la tarjeta no es válida según las reglas de la red emisora y Luhn
        Result = new CreditCardResult(foundNetwork != null ? foundNetwork.Name : "Not Found", false);
        return ValidationResultType.Ok;
    }

    public static string FindIssuingNetwork(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList)
    {
        string issuingNetworkName = "";
        bool startsWithNumbers = false;
        bool startsWithNumbersInRange = false;

        foreach (IssuingNetworkData issuingNetworkData in issuingNetworkDataList)
        {
            // Comprueba si el número de tarjeta de crédito comienza con ciertos números específicos
            if (issuingNetworkData.StartsWithNumbers?.Any() ?? false)
            {
                startsWithNumbers = StartsWithNumber(creditCardNumber, issuingNetworkData.StartsWithNumbers);
            }

            // Comprueba si el número de tarjeta de crédito está dentro de un rango específico
            if (issuingNetworkData.InRange != null)
            {
                startsWithNumbersInRange = StartsWithNumberInRange(creditCardNumber, issuingNetworkData.InRange);
            }

            // Si el número de tarjeta de crédito coincide con alguno de los criterios, establece la red emisora correspondiente y sale del bucle
            if (startsWithNumbers || startsWithNumbersInRange)
            {
                issuingNetworkName = issuingNetworkData.Name;
                break;
            }
        }

        return issuingNetworkName;
    }

    private static bool StartsWithNumber(string creditCardNumber, IEnumerable<int> numbers)
    {
        foreach (int number in numbers)
        {
            string numberString = number.ToString();
            if (creditCardNumber.StartsWith(numberString))
            {
                return true;
            }
        }
        return false;
    }

    private static bool StartsWithNumberInRange(string creditCardNumber, RangeNumber rangeNumber)
    {
        int numberLength = rangeNumber.MinValue.ToString().Length;
        // Verifica si el número de tarjeta de crédito tiene suficientes dígitos para comparar con el rango
        if (creditCardNumber.Length < numberLength)
        {
            return false;
        }

        int initialCreditCardDigits = int.Parse(creditCardNumber.Substring(0, numberLength));

        return (initialCreditCardDigits >= rangeNumber.MinValue) && (initialCreditCardDigits <= rangeNumber.MaxValue);
    }


    private ValidationResultType NetworkMatches(IssuingNetworkData network, string creditCardNumber)
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
            return ValidationResultType.Ok;
        }

        // Si InRange no es null, verifica si el número de tarjeta de crédito está en el rango especificado.
        if (network.InRange != null)
        {
            var range = network.InRange;
            int cardNumberPrefix;

            if (int.TryParse(creditCardNumber.Substring(0, range.MinValue.ToString().Length), out cardNumberPrefix))
            {
                if (cardNumberPrefix < range.MinValue || cardNumberPrefix > range.MaxValue)
                {
                    // El prefijo del número de tarjeta de crédito está fuera del rango permitido según las reglas de la red emisora.
                }
                else
                {
                    return ValidationResultType.Ok;
                }
            }
        }
        return ValidationResultType.NotFound;
    }

    public static bool IsValidCreditCardLength(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList, string issuingNetworkName)
    {
        // Encuentra la información de la red emisora específica por nombre
        IssuingNetworkData issuingNetworkData = issuingNetworkDataList.Single(issuingNetworkData => issuingNetworkData.Name.Trim() == issuingNetworkName.Trim());

        // Obtiene la longitud del número de tarjeta de crédito
        int creditCardNumberLength = creditCardNumber.Trim().Length;

        // Comprueba si la longitud es una de las longitudes permitidas según las reglas de la red emisora
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
                    return prefixString;
                }
            }
        }
        return string.Empty; 
    }

    private static bool IsValidCreditCardNumber(string creditCardNumber)
    {
        return Regex.IsMatch(creditCardNumber, NUMBER_REGEX);
    }

    private string AddSpacesBetweenLowerAndCapitalLetters(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];

            if (i > 0 && char.IsUpper(currentChar) && char.IsLower(input[i - 1]))
            {
                result.Append(' ');
            }

            result.Append(currentChar);
        }

        return result.ToString();
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