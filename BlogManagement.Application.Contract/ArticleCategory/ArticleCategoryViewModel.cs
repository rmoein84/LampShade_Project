﻿namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }
        public long ArticlesCount { get; set; }
    }
}
