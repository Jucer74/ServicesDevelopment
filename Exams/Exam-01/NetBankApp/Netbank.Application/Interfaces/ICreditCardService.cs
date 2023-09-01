using NetBank.Domain.Define;
using NetBank.Domain.Dto;

namespace Netbank.Application.Interfaces
{
    public interface ICreditCardService
    {
        public Task<ValidationResultType> Validate(string creditCardNumber);
        public Task<List<IssuingNetworkData>> LoadIssuingNetworkData();
        public CreditCardResult Result { get; set; }
    }
}