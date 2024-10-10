namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool NotInStock { get; set; }
    }
}

