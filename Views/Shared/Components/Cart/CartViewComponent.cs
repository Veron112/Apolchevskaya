using Microsoft.AspNetCore.Mvc;

namespace Apolchevskaya.Views.Shared.Components.Cart
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        { return View(); }
    }
}
