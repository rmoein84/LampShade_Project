using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using CommentManagment.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShade.Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleBy(string slug)
        {
            var article = _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryName = x.Category.Title,
                    CategorySlug = x.Category.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    PublishDate = x.PublishDate.ToFarsi(),

                }).FirstOrDefault(x => x.Slug == slug);
            article.Comments = _commentContext.Comments
                .Where(x => x.Status == Statuses.Confirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi()
                })
                .OrderByDescending(x => x.Id)
                .ToList();
            return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),

                }).OrderByDescending(x => x.Id)
            .Take(3)
            .ToList();
        }
    }
}
