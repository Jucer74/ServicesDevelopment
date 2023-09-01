using NetBank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetBank.Application.Services
{
    public class CreditCardValidationService : ICreditCardValidationService
    {
        private readonly IIssuingNetworkRepository _issuingNetworkRepository;
        private const string NUMBER_REGEX = "^[0-9]*$";

        public CreditCardValidationService(IIssuingNetworkRepository issuingNetworkRepository)
        {
            _issuingNetworkRepository = issuingNetworkRepository;
        }

        public async Task<ValidationResult> ValidateCreditCardAsync(string creditCardNumber)
        {
            bool isValidCreditCardLength;
            bool isCreditCardNumberValid;
            string issuingNetworkName;
            List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkDataAsync();

            if (!Regex.IsMatch(creditCardNumber, NUMBER_REGEX))
            {
                return ValidationResult.BadRequest("Invalid credit card number format.");
            }

            issuingNetworkName = FindIssuingNetwork(creditCardNumber, issuingNetworkDataList);

            if (string.IsNullOrEmpty(issuingNetworkName))
            {
                return ValidationResult.NotFound("Issuing network not found.");
            }

            isValidCreditCardLength = IsValidCreditCardLength(creditCardNumber, issuingNetworkDataList, issuingNetworkName);
            isCreditCardNumberValid = IsValidCreditCardNumber(creditCardNumber);

            if (isValidCreditCardLength && isCreditCardNumberValid)
            {
                return ValidationResult.Ok(issuingNetworkName, true);
            }

            return ValidationResult.Ok(issuingNetworkName, false);
        }

        private async Task<List<IssuingNetworkData>> LoadIssuingNetworkDataAsync()
        {
            List<IssuingNetwork> issuingNetworkList = await GetIssuingNetworksAsync();
            return issuingNetworkList.Select(MapIssuingNetworkToData).ToList();
        }

        private async Task<List<IssuingNetwork>> GetIssuingNetworksAsync()
        {
            return (await _issuingNetworkRepository.GetAllAsync()).ToList();
        }

        private string FindIssuingNetwork(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList)
        {
            // Implementa tu lógica para encontrar la red emisora aquí.
            // Puedes utilizar la lista issuingNetworkDataList y el número de tarjeta de crédito proporcionado.
            // Devuelve el nombre de la red emisora o null si no se encuentra.
            return "ExampleNetwork";
        }

        private bool IsValidCreditCardLength(string creditCardNumber, List<IssuingNetworkData> issuingNetworkDataList, string issuingNetworkName)
        {
            // Implementa tu lógica para validar la longitud de la tarjeta de crédito aquí.
            // Puedes utilizar la lista issuingNetworkDataList y el nombre de la red emisora proporcionado.
            // Devuelve true si la longitud es válida, de lo contrario, false.
            return true;
        }

        private bool IsValidCreditCardNumber(string creditCardNumber)
        {
            // Implementa tu lógica para validar el número de tarjeta de crédito aquí.
            // Puedes utilizar el número de tarjeta de crédito proporcionado.
            // Devuelve true si el número es válido, de lo contrario, false.
            return true;
        }

        private IssuingNetworkData MapIssuingNetworkToData(IssuingNetwork issuingNetwork)
        {
            // Implementa tu lógica de mapeo aquí.
            // Puedes mapear un objeto IssuingNetwork a un objeto IssuingNetworkData.
            return new IssuingNetworkData();
        }
    }
}
