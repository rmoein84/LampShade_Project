using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.OrderAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderApplication _orderApplication;
    private readonly IAccountApplication _accountApplication;
    public List<OrderViewModel> Orders { get; set; }
    public OrderSearchModel SearchModel { get; set; }
    public SelectList Accounts;
    public IndexModel(IOrderApplication productCategoryApplication, IAccountApplication accountApplication)
    {
        _orderApplication = productCategoryApplication;
        _accountApplication = accountApplication;
    }

    public void OnGet(OrderSearchModel searchModel)
    {
        Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "Fullname");
        Orders = _orderApplication.Search(searchModel);
    }
    public IActionResult OnGetConfirm(long id)
    {
        _orderApplication.PaymentSucceeded(id, 0);
        return RedirectToPage("./Index");
    }
    public IActionResult OnGetCancel(long id)
    {
        _orderApplication.Cancel(id);
        return RedirectToPage("./Index");
    }
    public IActionResult OnGetItems(long id){
        var orderItems = _orderApplication.GetItems(id);
        return Partial("./Items", orderItems);
    }
}
