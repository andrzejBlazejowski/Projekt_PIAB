using Projekt.Data.Data.Shop;

namespace Projekt.portalWWW.Models
{

    public class CheckoutData
    {
        public List<CheckoutItem> CheckoutItems { get; set; }
        public decimal Total { get; set; }

    }
}
