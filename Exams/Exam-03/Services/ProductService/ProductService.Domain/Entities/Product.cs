using ProductService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities
{
    public class Product : EntityBase
    {
        [Required(ErrorMessage = "CategoryId is Required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(50, ErrorMessage = "Category Name's Max Length is 50 Characters")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "EanCode is Required")]
        [StringLength(13, ErrorMessage = "EanCode's Max Length is 13 digits")]
        public string? EanCode { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Unit is Required")]
        [StringLength(20, ErrorMessage = "Unit's Max Length is 20 Characters")]
        public string? Unit { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; set; }
    }
}
