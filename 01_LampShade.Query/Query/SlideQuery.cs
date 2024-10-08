using _01_LampShade.Query.Contracts.Slide;
using ShopManagement.Infrastructere.EFCore;

namespace _01_LampShade.Query.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(x => !x.IsRemoved).Select(x => new SlideQueryModel
            {
                Picture = x.Picture,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Link = x.Link,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title
            }).ToList();
        }
    }
}
