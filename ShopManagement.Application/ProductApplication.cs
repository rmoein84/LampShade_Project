using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var picturePath = $"{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            var product = new Product(command.Title, command.Code, command.ShortDescription,
                command.Description, pictureName, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var product = _productRepository.GetWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var slug = command.Slug.Slugify();
            var picturePath = $"{product.Category.Slug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            product.Edit(command.Title, command.Code, command.ShortDescription,
                command.Description, pictureName, command.PictureAlt, command.PictureTitle,
                slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        public List<ProductViewModel> Search(ProductSearchModel searchMoodel)
        {
            return _productRepository.Search(searchMoodel);
        }
    }
}
