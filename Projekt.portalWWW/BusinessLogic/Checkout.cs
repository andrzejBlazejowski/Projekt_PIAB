using Microsoft.EntityFrameworkCore;
using Projekt.Data.Data;
using Projekt.Data.Data.Shop;

namespace Projekt.portalWWW.BusinessLogic
{
    public class Checkout
    {
        private readonly ProjectContext _context;
        private string SessionId { get; set; }
        public Checkout(ProjectContext context, HttpContext httpContext)
        {
            _context = context;
            SessionId = GetSessionId(httpContext);
            SessionId = GetSessionId(httpContext);
        }

        private string GetSessionId(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("SessionId") == null)
            {
                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    httpContext.Session.SetString("SessionId", httpContext.User.Identity.Name);
                }
                else
                {
                    httpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
                }
            }
            return httpContext.Session.GetString("SessionId");
        }

        public async Task AddAsync(int productId)
        {
            CheckoutItem checkoutItem = _context.CheckoutItem
                .Where(p => p.SessionId == SessionId && p.ProductId == productId)
                .FirstOrDefault();

            if (checkoutItem == null)
            {
                Product product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
                checkoutItem = new CheckoutItem()
                {
                    SessionId = this.SessionId,
                    ProductId = productId,
                    Product = product,
                    Quantity = 1,
                    Name = product.Name,
                    LastModificationDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    LastModifiedBy = 1,
                    CreatedBy = 1,
                    IsActive = true,
                };
                _context.CheckoutItem.Add(checkoutItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                UpdateItemQuantity(checkoutItem.Id, checkoutItem.Quantity + 1);
            }
        }

        public async Task<List<CheckoutItem>> getCheckoutItems()
        {
            return await _context.CheckoutItem
                .Where(p => p.SessionId == SessionId && p.IsActive == true)
                .Include(p => p.Product)
                    .ThenInclude(p => p.Image)
                .ToListAsync();
        }

        public async Task<decimal> getTotalPrice()
        {
            return await _context.CheckoutItem
                .Where(p => p.SessionId == SessionId && p.IsActive == true)
                .Select(p => p.Product.Price * p.Quantity)
                .SumAsync();
        }

        public async Task<CheckoutItem> getCheckoutItemById(int id)
        {
            return await _context.CheckoutItem
                .Where(p => p.SessionId == SessionId && p.Id == id)
                .Include(p => p.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateItemQuantity(int id, int quantity)
        {
            CheckoutItem checkoutItem = await getCheckoutItemById(id);
            if (checkoutItem != null)
            {
                if (quantity > 0)
                {
                    checkoutItem.IsActive = true;
                }
                else
                {
                    checkoutItem.IsActive = false;
                }
                checkoutItem.Quantity = quantity;
                checkoutItem.LastModificationDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> IncreaseItemQuantity(int id) 
        {
            CheckoutItem checkoutItem = await getCheckoutItemById(id);
            if (checkoutItem != null)
            {
                return await UpdateItemQuantity(id, checkoutItem.Quantity + 1);
            }
            return false;
        }


        public async Task<bool> DecreaseItemQuantity(int id)
        {
            {
                CheckoutItem checkoutItem = await getCheckoutItemById(id);
                if (checkoutItem != null)
                {
                    return await UpdateItemQuantity(id, checkoutItem.Quantity - 1);
                }
                return false;
            }
        }

        public async Task<bool> DeleteCheckoutItem(int id)
        {
            CheckoutItem checkoutItem = await getCheckoutItemById(id);
            if (checkoutItem != null)
            {
                checkoutItem.IsActive = false;
                checkoutItem.LastModificationDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
