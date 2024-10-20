using _0_Framework.Application;
using System.Xml.Linq;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double UnitPrice { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public double TotalItemPrice { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }

        public bool IsInStock { get; set; }

        public CartItem(long id, string title, double unitPrice, string picture, int count)
        {
            Id = id;
            Title = title;
            UnitPrice = unitPrice;
            Picture = picture;
            Count = count;
            TotalItemPrice = unitPrice * count;
        }
    }
}
