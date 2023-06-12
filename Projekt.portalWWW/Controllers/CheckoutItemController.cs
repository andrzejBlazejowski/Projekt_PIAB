using Projekt.Data.Data;
using Projekt.portalWWW.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Projekt.portalWWW.Models;

namespace Projekt.portalWWW.Controllers
{
    public class CheckoutItemController : BaseController
    {
        private Checkout _checkout;
        public Checkout checkout {
            get {
                if(_checkout == null && this.HttpContext != null)
                {
                    _checkout = new Checkout(this._context, this.HttpContext);
                }
                return _checkout;
            } 
            set 
            {
                if(_checkout != value && value != null)
                {
                    _checkout = value;
                }
            } 
        }
        public CheckoutItemController(ProjectContext context) : base(context) { }

        public override async Task<IActionResult> Index(int Id)
        {
            prepareLayoutData();

            CheckoutData daneDoKoszyka = new CheckoutData
            {
                CheckoutItems = await checkout.getCheckoutItems(),
                Total = await checkout.getTotalPrice(),
            };

            return View(daneDoKoszyka);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Checkout _checkout = new Checkout(this._context, this.HttpContext);

            await checkout.DeleteCheckoutItem(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            Checkout _checkout = new Checkout(this._context, this.HttpContext);

            await checkout.UpdateItemQuantity(id, quantity);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> IncreaseQuantity(int id)
        {
            await checkout.IncreaseItemQuantity(id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            await checkout.DecreaseItemQuantity(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            Checkout _checkout = new Checkout(this._context, this.HttpContext);

            checkout.AddAsync(productId);

            return RedirectToAction("Index");
        }
    }
}
