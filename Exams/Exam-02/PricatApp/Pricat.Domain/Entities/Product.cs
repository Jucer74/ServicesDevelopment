using Pricat.Domain.Exceptions;

namespace Pricat.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string EanCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public void SetEanCode(string eanCode)
        {
            if (!IsValidEan13(eanCode))
            {
                throw new DomainException(
                    "INVALID_EAN_CODE",
                    "El código EAN debe tener 13 dígitos válidos"
                );
            }
            EanCode = eanCode;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new DomainException(
                    "INVALID_PRICE",
                    "El precio debe ser mayor a cero"
                );
            }
            Price = newPrice;
        }

        private static bool IsValidEan13(string eanCode)
        {
            return eanCode.Length == 13 && eanCode.All(char.IsDigit);
        }
    }
}