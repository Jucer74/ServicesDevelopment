using System.Text.RegularExpressions;
using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;


namespace Netbank.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IIssuingNetworkRepository _issuingNetworkRepository;
        private const string NUMBER_REGEX = "^[0-9]*$";

        public CreditCardResult Result { get; set; } = null!;

        public CreditCardService(IIssuingNetworkRepository issuingNetworkRepository)
        {
            _issuingNetworkRepository = issuingNetworkRepository;
        }

        public async Task<ValidationResultType> Validate(string creditCardNumber)
        {
            var issuingNetworkDataList = await LoadIssuingNetworkData();

            if (!IsValidCreditCardNumber(creditCardNumber))
            {
                Result = new CreditCardResult("Bad Request", false);
                return ValidationResultType.BadRequest;
            }

            var creditCardLength = creditCardNumber.Length;
            IssuingNetworkData? foundNetwork = null;

            string issuingNetworkName = FindIssuingNetwork(creditCardNumber, issuingNetworkDataList);

            if (string.IsNullOrEmpty(issuingNetworkName))
            {
                Result = new CreditCardResult("Not Found", false);
                return ValidationResultType.ok;
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
                bool isValidCreditCardLength = IsValidCreditCardLength(creditCardNumber, issuingNetworkDataList, foundNetwork.Name);
                bool isCreditCardNumberValid = CreditCardValidator.IsValid(creditCardNumber);

                if (isValidCreditCardLength && isCreditCardNumberValid)
                {
                    Result = new CreditCardResult(foundNetwork.Name, true);
                    return ValidationResultType.Ok;
                }
            }

            Result = new CreditCardResult(issuingNetworkName ?? "Not Found", false);
            return ValidationResultType.Ok;
        }

        private static string FindIssuingNetwork(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList)
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

            if (creditCardNumber.Length < numberLength)
            {
                return false;
            }

            int initalCreditCardDigits = int.Parse(creditCardNumber[..numberLength]);

            return (initalCreditCardDigits >= rangeNumber.MinValue) && (initalCreditCardDigits <= rangeNumber.MaxValue);
        }

        private static bool StartsWithNumberFromList(string creditCardNumber, List<int> numberList)
        {
            int numberLength;
            int initalCreditCardDigits;

            foreach (int number in numberList)
            {
                numberLength = number.ToString().Length;

                if (creditCardNumber.Length < numberLength)
                {
                    continue;
                }

                initalCreditCardDigits = int.Parse(creditCardNumber[..numberLength]);

                if (initalCreditCardDigits == number)
                {
                    return true;
                }
            }

            return false;
        }

        private ValidationResultType NetworkMatches(IssuingNetworkData network, string creditCardNumber, int creditCardLength)
        {
            if (!IsValidCreditCardNumber(creditCardNumber))
            {
                return ValidationResultType.BadRequest;
            }

            var matchingPrefix = FindMatchingPrefix(network, creditCardNumber);

            if (!string.IsNullOrEmpty(matchingPrefix))
            {
                if (network.AllowedLengths != null && network.AllowedLengths.Contains(creditCardLength))
                {
                    return ValidationResultType.Ok;
                }
                else
                {
                    return ValidationResultType.Ok;
                }
            }

            if (network.InRange != null)
            {
                var range = network.InRange;
                int cardNumberPrefix;

                if (int.TryParse(creditCardNumber.Substring(0, range.MinValue.ToString().Length), out cardNumberPrefix))
                {
                    if (cardNumberPrefix >= range.MinValue && cardNumberPrefix <= range.MaxValue)
                    {
                        if (network.AllowedLengths != null && network.AllowedLengths.Contains(creditCardLength))
                        {
                            return ValidationResultType.Ok;
                        }
                        else
                        {
                            return ValidationResultType.Ok;
                        }
                    }
                    else
                    {
                        return ValidationResultType.Ok;
                    }
                }
            }

            return ValidationResultType.ok;
        }

        private static bool IsValidCreditCardLength(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList, string issuingNetworkName)
        {
            IssuingNetworkData issuingNetworkData = issuingNetworkDataList.Single(issuingNetworkData => issuingNetworkData.Name.Trim() == issuingNetworkName.Trim());
            int creditCardNumberLength = creditCardNumber.Trim().Length;

            return issuingNetworkData.AllowedLengths.Contains(creditCardNumberLength);
        }

        private string FindMatchingPrefix(IssuingNetworkData network, string creditCardNumber)
        {
            if (network.StartsWithNumbers != null && network.StartsWithNumbers.Any())
            {
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
}






