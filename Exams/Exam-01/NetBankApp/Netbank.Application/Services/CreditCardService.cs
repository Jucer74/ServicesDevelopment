using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using NetBank.Utilities;
using NetBank.Domain.Interfaces.Repositories;
using Netbank.Application.Map;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netbank.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IIssuingNetworkRepository _issuingNetworkRepository;

        // Expresión regular para validar solo números
        public const string NUMBER_REGEX = "^[0-9]*$";

        public CreditCardResult Result { get; set; } = null!;

        public CreditCardService(IIssuingNetworkRepository issuingNetworkRepository)
        {
            _issuingNetworkRepository = issuingNetworkRepository;
        }

        public async Task<ValidationResultType> Validate(string creditCardNumber)
        {
            ValidationResultType validationResultType;
            bool isValidCreditCard = false;
            string? foundIssuingNetworkDataName;
            List<IssuingNetworkData> issuingNetworkDataList = await LoadIssuingNetworkData();

            // Comprobar si el número de tarjeta es numérico
            if (IsNumeric(creditCardNumber))
            {
                // Identificar la red emisora primero
                foundIssuingNetworkDataName = FindIssuingNetworkOwnerName(issuingNetworkDataList, creditCardNumber);

                // Luego verificar si el número de tarjeta es válido
                isValidCreditCard = CreditCardValidator.IsValid(creditCardNumber);

                if (foundIssuingNetworkDataName != null)
                {
                    validationResultType = ValidationResultType.Ok;
                }
                else
                {
                    validationResultType = ValidationResultType.NotFound;
                    foundIssuingNetworkDataName = "Not Found";
                }
            }
            else
            {
                // El número de tarjeta no es numérico
                validationResultType = ValidationResultType.BadRequest;
                foundIssuingNetworkDataName = "Bad Request";
            }

            // Establecer el resultado y devolver el tipo de validación
            Result = new CreditCardResult(foundIssuingNetworkDataName, isValidCreditCard);
            return validationResultType;
        }

        // Método para verificar si una cadena es numérica
        private bool IsNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }

        // Método para encontrar el nombre del propietario de la red emisora
        private static string? FindIssuingNetworkOwnerName(List<IssuingNetworkData> issuingNetworkDataList, string creditCardNumber)
        {
            var foundIssuingNetworkData = issuingNetworkDataList.Find(issuingNetworkData => issuingNetworkData.ValidateCreditCard(creditCardNumber));
            return foundIssuingNetworkData?.Name;
        }

        // Método para cargar datos de la red emisora
        private async Task<List<IssuingNetworkData>> LoadIssuingNetworkData()
        {
            List<IssuingNetwork> issuingNetworks = await GetIssuingNetworks();
            List<IssuingNetworkData> issuingNetworkDataList = IMapp.ToIssuingNetworkDataList(issuingNetworks);
            // Convertir datos a lista de datos
            return issuingNetworkDataList;
        }

        // Método para obtener redes emisoras
        private async Task<List<IssuingNetwork>> GetIssuingNetworks()
        {
            // Cargar datos desde la base de datos
            IEnumerable<IssuingNetwork> issuingNetworks = await _issuingNetworkRepository.GetAllAsync();
            return issuingNetworks.ToList();
        }
    }
}







