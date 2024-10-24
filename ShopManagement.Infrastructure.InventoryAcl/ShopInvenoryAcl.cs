using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInvenoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInvenoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool DecreaseFromInventory(List<OrderItem> items)
        {
            var command = new List<DecreaseInventory>();
            foreach (var item in items)
            {
                var decrease = new DecreaseInventory(item.ProductId, item.Count, "خرید مشتری", item.OrderId);
                command.Add(decrease);
            }
            return _inventoryApplication.Decrease(command).IsSuccedded;
        }
    }
}
