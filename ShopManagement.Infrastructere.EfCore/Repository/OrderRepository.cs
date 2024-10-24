using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructere.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;
            return 0;

        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x=> new {x.Id, x.Title}).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();
            var items = order.Items.Select(x=> new OrderItemViewModel
            {
                Id = x.Id,
                OrderId = x.OrderId,
                DiscountRate = x.DiscountRate,
                Count = x.Count,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).ToList();
            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x=> x.Id == item.ProductId).Title;
            }
            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                IssueTrackingNo = x.IssueTrackingNo,
                PaymentMethodId = x.PaymentMethod,
                DiscountAmount = x.DiscountAmount,
                TotalAmount = x.TotalAmount,
                IsCanceled = x.IsCanceled,
                AccountId = x.AccountId,
                PayAmount = x.PayAmount,
                IsPaid = x.IsPaid,
                RefId = x.RefId,
            }).Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            var result = query.OrderByDescending(x => x.Id).ToList();
            foreach (var item in result)
            {
                item.AccountFullName = accounts.FirstOrDefault(x => x.Id == item.AccountId).Fullname;
                item.PaymentMethod = PaymentMethod.GetBy(item.PaymentMethodId).Name;
            }
            return result;
        }
    }
}
