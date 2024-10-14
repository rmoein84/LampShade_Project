using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = id,
                Title = x.Title,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                CateogryId = x.CategoryId,
                PublishDate = x.PublishDate.ToFarsi(),
                CanonicalAddress = x.CanonicalAddress,
            })
                .FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles
                .Include(x => x.Category)
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    Cateogry = x.Category.Title,
                    CategoryId = x.CategoryId,
                    Title = x.Title,
                    Picture = x.Picture,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            if (searchModel.CateogryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CateogryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
