using System.Text.RegularExpressions;
using NetBank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Utilities;

namespace NetBank.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IIssuingNetworkRepository _issuingNetworkRepository;
        private const string NumberRegex = "^[0-9]*$";

        public CreditCardService(IIssuingNetworkRepository issuingNetworkRepository)
        {
            _issuingNetworkRepository = issuingNetworkRepository;
        }

        public CreditCardResult Result { get; set; } = null!;

        public async Task<ValidationResultType> Validate(string creditCardNumber)
        {
            try
            {
                // Load issuing network data from database
                List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();

                // Get credit card length
                int cardLength = creditCardNumber.Length;
                // Invalid network name
                string? invalidNetwork = null;
                // Check if credit card number is valid
                bool isValid = CreditCardValidator.IsValid(creditCardNumber);

                // Check if there is any letter in credit card number
                if (!Regex.IsMatch(creditCardNumber, NumberRegex))
                {
                    Result = new CreditCardResult("Bad Request", isValid);
                    return ValidationResultType.BadRequest;
                }

                // Loop through each issuing network
                foreach (var network in issuingNetworkDataList)
                {
                    // Check if the card starts with any of the numbers in the list
                    bool startsWithNumberMatch =
                        network.StartsWithNumbers?.Exists(number => creditCardNumber.StartsWith(number.ToString())) ==
                        true;
                    // Check if the card is in range
                    bool isInRange = network.InRange != null &&
                                     CreditCardValidator.IsInRange(creditCardNumber, network.InRange);
                    // Check if the card length is allowed
                    bool hasAllowedLength = network.AllowedLengths.Contains(cardLength);

                    // Check if the card matches this network's criteria
                    if ((startsWithNumberMatch || isInRange) && hasAllowedLength)
                    {
                        // Card is valid for this network
                        await GetIssuingNetworkData(network.Id);
                        Result = new CreditCardResult(network.Name, isValid);
                        return ValidationResultType.Ok;
                    }
                    else if (!hasAllowedLength)
                    {
                        // Card length is not allowed for this network
                        invalidNetwork = network.Name;
                    }
                }

                if (!string.IsNullOrEmpty(invalidNetwork))
                {
                    // Card length is invalid for the specified network
                    Result = new CreditCardResult($"{invalidNetwork}", false);
                    return ValidationResultType.Ok;
                }

                // Credit card number is not in range or doesn't have an allowed length, mark as not valid
                Result = new CreditCardResult("Not Found", false); // Mark as not valid
                return ValidationResultType.NotFound;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during credit card validation: {ex.Message}");
                throw;
            }
        }

        // Get issuing network data from database
        private async Task GetIssuingNetworkData(int id)
        {
            // Cache issuing network data
            IssuingNetwork? issuingNetwork = await _issuingNetworkRepository.GetByIdAsync(id);


            // If issuing network is null, throw exception
            if (issuingNetwork != null)
            {
                // Interface result
                Result = new CreditCardResult(issuingNetwork.Name, true);
            }
        }

        // Load issuing network data from database
        private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
        {
            // Cache issuing network data
            IEnumerable<IssuingNetwork> issuingNetworks = await _issuingNetworkRepository.GetAllAsync();

            // Map to IssuingNetworkData
            return issuingNetworks.Select(network => new IssuingNetworkData
            {
                Id = network.Id,
                Name = network.Name,
                StartsWithNumbers = network.StartsWithNumbers?.Split(',').Select(int.Parse).ToList(),
                AllowedLengths = network.AllowedLengths.Split(',').Select(int.Parse).ToList(),
                InRange = CreditCardValidator.ParseRange(network.InRange)
            }).ToList();
        }

        // ICreditCardService shared result property
        Task<ValidationResultType> ICreditCardService.Validate(string creditCardNumber)
        {
            return Validate(creditCardNumber);
        }
    }
}