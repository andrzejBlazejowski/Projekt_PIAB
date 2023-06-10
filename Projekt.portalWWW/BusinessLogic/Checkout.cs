using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public void DodajDoKoszyka(Product product)
        {
            CheckoutItem checkoutItem = _context.CheckoutItem
                .Where(p => p.SessionId == SessionId && p.ProductId == product.Id)
                .FirstOrDefault();

            if (checkoutItem == null)
            {
                checkoutItem = new CheckoutItem()
                {
                    SessionId = this.SessionId,
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1,
                    LastModificationDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    LastModifiedBy = 1,
                    CreatedBy = 1                
                };
                _context.CheckoutItem.Add(checkoutItem);
            }
            else
            {
                checkoutItem.Quantity++;
                _context.SaveChanges();

            }
        }

        public async Task<List<CheckoutItem>> getCheckoutItem()
        {
            return await _context.CheckoutItem
                .Where(p => p.SessionId == SessionId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<decimal> getWartoscKoszyka()
        {
            return await _context.CheckoutItem
                .Where(p => p.SessionId == SessionId)
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

        public async Task<bool> UpdateCheckoutItem(int id, int quantity)
        {
            CheckoutItem checkoutItem = await getCheckoutItemById(id);
            if (checkoutItem != null)
            {
                checkoutItem.Quantity = quantity;
                checkoutItem.LastModificationDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCheckoutItem(int id)
        {
            CheckoutItem checkoutItem = await getCheckoutItemById(id);
            if (checkoutItem != null)
            {
                _context.CheckoutItem.Remove(checkoutItem);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
