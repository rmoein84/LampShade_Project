using _01_LampShade.Query.Contracts.Inventory;
using InventoryManagement.Infrasturcture.EFCore;
using ShopManagement.Infrastructere.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShade.Query.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var productName = _shopContext.Products.Select(x => new { x.Id, x.Title })
                    .FirstOrDefault(x => x.Id == command.ProductId).Title;
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = productName
                };
            }
            return new StockStatus
            {
                IsStock = true,
            };
        }
    }
}
