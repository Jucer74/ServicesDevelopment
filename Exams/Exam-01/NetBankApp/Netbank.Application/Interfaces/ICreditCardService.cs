using NetBank.Domain.Define;
using NetBank.Domain.Dto;

namespace Netbank.Application.Interfaces
{

    public interface ICreditCardService
    {
        string IdentifyIssuingNetwork(string creditCardNumber);

        public Task<ValidationResultType> Validate(string creditCardNumber);
        public CreditCardResult Result { get; set; }
    }
}