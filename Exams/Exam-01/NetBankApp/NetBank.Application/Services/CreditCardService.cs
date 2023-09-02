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

                // Check if credit card number is valid
                bool isValid = CreditCardValidator.IsValid(creditCardNumber);
                
                // Check if there is any letter in credit card number
                if (!Regex.IsMatch(creditCardNumber, NumberRegex))
                {
                    Result = new CreditCardResult("Bad Request", isValid);
                    return ValidationResultType.BadRequest;
                }
                
                // If credit card number is not valid, return invalid
                if (!isValid)
                {
                    foreach (var network in issuingNetworkDataList)
                    {
                        if (network.StartsWithNumbers?.Exists(number => creditCardNumber.StartsWith(number.ToString())) ==
                            true ||
                            (network.InRange != null &&
                             CreditCardValidator.IsInRange(creditCardNumber, network.InRange)))
                        {
                            await GetIssuingNetworkData(network.Id);
                            Result = new CreditCardResult(network.Name, isValid);
                            return ValidationResultType.Ok;
                        }
                    }
                }
                
                // Credit card number is valid, check if it is in range and return result
                foreach (var network in issuingNetworkDataList)
                {
                    if (network.StartsWithNumbers?.Exists(number => creditCardNumber.StartsWith(number.ToString())) ==
                        true ||
                        (network.InRange != null && CreditCardValidator.IsInRange(creditCardNumber, network.InRange)))
                    {
                        await GetIssuingNetworkData(network.Id);
                        Result = new CreditCardResult(network.Name, isValid);
                        return ValidationResultType.Ok;
                    }
                }
                
                // Credit card number is not valid and not in range, return not found
                Result = new CreditCardResult("Not Found", isValid);
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

            // Interface result
            Result = new CreditCardResult(issuingNetwork.Name, true);
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