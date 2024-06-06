using Apolchevskaya.Extensions;
using Microsoft.AspNetCore.Mvc;
using MailChimp.Net.Models;
using Newtonsoft.Json;

namespace Apolchevskaya.Components
{
    public class CartViewComponent: ViewComponent
	{
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Ski.Domain.Cart.Cart>("cart");
            return View(cart);
        }
	}
}
