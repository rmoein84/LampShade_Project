using _01_LampShade.Query.Contracts.Product;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            command.Type = CommentType.Product;
            _commentApplication.Add(command);
            return RedirectToPage("/Product", new { slug = ProductSlug });
        }

    }
}
