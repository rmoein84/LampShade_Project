using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        EditInventory GetDetails(long id);
        //OperationResult IsInStock(long id);
        //OperationResult NotInStock(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
