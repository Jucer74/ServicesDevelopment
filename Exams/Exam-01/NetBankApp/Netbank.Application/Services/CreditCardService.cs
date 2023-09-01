using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Utilities;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            bool isValid;
            string issuingNetworkName = string.Empty;

            List<IssuingNetwork> issuingNetworksList = await GetIssuingNetworks();

            List<IssuingNetworkData> issuingNetworkDataList = LoadIssuingNetworks(issuingNetworksList);

            int digitos = creditCardNumber.Length;


            if (IsNumeric(creditCardNumber) && !string.IsNullOrEmpty(creditCardNumber))
            {
                foreach (IssuingNetworkData issuingNetwork in issuingNetworkDataList)
                {
                    if (CardFilterValidate.SearchIssuingNetwork(creditCardNumber, issuingNetwork))
                    {
                        issuingNetworkName = issuingNetwork.Name!;

                        if (CardFilterValidate.VerifyLength(creditCardNumber, issuingNetwork.AllowedLengths!))
                        {
                            isValid = CreditCardValidator.IsValid(creditCardNumber);
                            Result = new CreditCardResult(issuingNetworkName, isValid);
                            return ValidationResultType.Ok;
                        }
                        else
                        {
                            isValid = false;
                            Result = new CreditCardResult(issuingNetworkName, isValid);
                            return ValidationResultType.Ok;
                        }
                    }
                }
                isValid = false;
                issuingNetworkName = "Not Found";
                Result = new CreditCardResult(issuingNetworkName, isValid);
                return ValidationResultType.NotFound;
            }
            else
            {
                isValid = false;
                issuingNetworkName = "Bad Request";
                Result = new CreditCardResult(issuingNetworkName, isValid);
                return ValidationResultType.BadRequest;
            }
        }


        private async Task<List<IssuingNetwork>> GetIssuingNetworks()
        {
            var issuingNetworks = await _issuingNetworkRepository.GetAllAsync();
            return issuingNetworks.ToList();
        }

        private List<IssuingNetworkData> LoadIssuingNetworks(List<IssuingNetwork> issuingNetworksList)
        {
            List<IssuingNetworkData> issuingNetworkDataList = new List<IssuingNetworkData>();
            // Iterar a través de la lista de objetos IssuingNetwork
            foreach (var issuingNetwork in issuingNetworksList)
            {
                var issuingNetworkData = new IssuingNetworkData
                {
                    Name = issuingNetwork.Name,
                    StartsWithNumbers = !string.IsNullOrEmpty(issuingNetwork.StartsWithNumbers)
                        ? issuingNetwork.StartsWithNumbers.Split(',').Select(int.Parse).ToList()
                        : null,
                    InRange = !string.IsNullOrEmpty(issuingNetwork.InRange)
                        ? new RangeNumber
                        {
                            MinValue = int.Parse(issuingNetwork.InRange.Split('-')[0]),
                            MaxValue = int.Parse(issuingNetwork.InRange.Split('-')[1])
                        }
                        : null,
                    AllowedLengths = issuingNetwork.AllowedLengths.Split(',').Select(int.Parse).ToList()
                };

                issuingNetworkDataList.Add(issuingNetworkData);
            }
            return issuingNetworkDataList;
        }




        

    static bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, NUMBER_REGEX);
        }
    }
}
