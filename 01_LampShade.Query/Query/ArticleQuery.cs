using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShade.Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleQueryModel GetArticleBy(string slug)
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
