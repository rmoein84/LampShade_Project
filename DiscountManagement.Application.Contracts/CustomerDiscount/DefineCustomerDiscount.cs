using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequire)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }

}
