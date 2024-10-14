using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Title = x.Title,  
                ShowOrder = x.ShowOrder,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                Description = x.Description,
            })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Select(x=>new ArticleCategoryViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreationDate = x.CreationDate.ToFarsi(),
                ShowOrder = x.ShowOrder,
                ArticlesCount = 0,
                Picture = x.Picture,
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x=>x.Title ==  searchModel.Title);

            return query.OrderByDescending(x=>x.ShowOrder).ToList();
        }
    }
}
