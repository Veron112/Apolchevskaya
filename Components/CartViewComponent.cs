using Apolchevskaya.Extensions;
using Microsoft.AspNetCore.Mvc;
using MailChimp.Net.Models;

namespace Apolchevskaya.Components
{
    public class CartViewComponent: ViewComponent
	{
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);
        }
    }
}
