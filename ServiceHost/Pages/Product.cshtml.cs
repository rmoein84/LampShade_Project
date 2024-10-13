using _01_LampShade.Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string slug)
        {
            Product = _productQuery.GetDetails(slug);
        }
        public IActionResult OnPost(AddComment command, string ProductSlug)
        {
            _commentApplication.Add(command);
            return RedirectToPage("./Product", new {slug = ProductSlug});
        }

    }
}
