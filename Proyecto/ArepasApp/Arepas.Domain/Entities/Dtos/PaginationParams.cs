
namespace Arepas.Domain.Entities.Dto
{
    public class PaginationParams
    {
        private const int _maxItemsPerPage = 20;
        private int itemsPerPage;

        public int Page { get; set; } = 1;

        public int Limit
        {
            get => itemsPerPage;
            set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}