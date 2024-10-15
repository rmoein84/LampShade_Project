using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory(command.Title, pictureName, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var articleCategory = _articleCategoryRepository.Get(command.Id);
            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit(command.Title, pictureName, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ArticleCategoryViewModel> GetAll()
        {
            return _articleCategoryRepository.GetAll();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
