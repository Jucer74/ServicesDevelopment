using System;
using System.Linq;
using System.Threading.Tasks;
using Netbank.Application.Interfaces;
using NetBank.Domain.Define;
using NetBank.Domain.Dto;
using System.Collections.Generic;

namespace Netbank.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        public CreditCardResult Result { get; set; } = new CreditCardResult("", false);

        public async Task<ValidationResultType> Validate(string creditCardNumber)
        {
            creditCardNumber = new string(creditCardNumber.Where(char.IsDigit).ToArray());

            if (!IsValidLuhn(creditCardNumber))
            {
                Result = new CreditCardResult("Bad Request", false);
                return ValidationResultType.BadRequest;
            }

            var issuingNetwork = GetIssuingNetwork(creditCardNumber);
            if (issuingNetwork == null)
            {
                Result = new CreditCardResult("Not Found", false);
                return ValidationResultType.NotFound;
            }

            Result = new CreditCardResult(issuingNetwork, true);
            return ValidationResultType.ValidationSuccess;
        }

        private bool IsValidLuhn(string creditCardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(creditCardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }


        private string GetIssuingNetwork(string creditCardNumber)
        {
            var networks = new Dictionary<string, string>
            {
                {"34", "American Express"},
                {"37", "American Express"},
                {"300", "Diners Club"},
                {"301", "Diners Club"},
                {"302", "Diners Club"},
                {"303", "Diners Club"},
                {"304", "Diners Club"},
                {"305", "Diners Club"},
                {"36", "Diners Club - International"},
                {"6011", "Discover"},
                {"622126-622925", "Discover"},
                {"644", "Discover"},
                {"645", "Discover"},
                {"646", "Discover"},
                {"647", "Discover"},
                {"648", "Discover"},
                {"649", "Discover"},
                {"65", "Discover"},
                {"637", "InstaPayment"},
                {"638", "InstaPayment"},
                {"639", "InstaPayment"},
                {"3528", "JCB"},
                {"3589", "JCB"},
                {"5018", "Maestro"},
                {"5020", "Maestro"},
                {"5038", "Maestro"},
                {"5893", "Maestro"},
                {"6304", "Maestro"},
                {"6759", "Maestro"},
                {"6761", "Maestro"},
                {"6762", "Maestro"},
                {"6763", "Maestro"},
                {"51", "MasterCard"},
                {"52", "MasterCard"},
                {"53", "MasterCard"},
                {"54", "MasterCard"},
                {"55", "MasterCard"},
                {"4", "Visa"},
                {"4026", "Visa Electron"},
                {"417500", "Visa Electron"},
                {"4508", "Visa Electron"},
                {"4844", "Visa Electron"},
                {"4913", "Visa Electron"},
                {"4917", "Visa Electron"}

              };

            foreach (var kvp in networks)
            {
                if (creditCardNumber.StartsWith(kvp.Key) &&
                    creditCardNumber.Length >= kvp.Key.Length)
                {
                    return kvp.Value;
                }
            }

            return "";
        }


    }
}






