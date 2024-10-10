using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long ProductId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
