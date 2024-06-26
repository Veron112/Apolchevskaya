﻿using Ski.Domain.Cart;
using Apolchevskaya.Extensions;
using Apolchevskaya.Extensions;
using Apolchevskaya.Services;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cart = Ski.Domain.Cart.Cart;

namespace Apolchevskaya.Controllers
{
	public class CartController : Controller
	{
		private readonly IProductService _productService;
		private Cart _cart;
		public CartController(IProductService productService)
		{
			_productService = productService;
		}
		// GET: CartController
		public ActionResult Index()
		{
			_cart = HttpContext.Session.Get<Cart>("cart") ?? new();
			ViewBag.Cart = HttpContext.Session.Get<Cart>("cart");
			return View(_cart.CartItems);
		}

		[Route("[controller]/add/{id:int}")]
		public async Task<ActionResult> Add(int id, string returnUrl)
		{
			var data = await _productService.GetProductByIdAsync(id);
			if (data.Success)
			{
				_cart = HttpContext.Session.Get<Cart>("cart") ?? new();
				_cart.AddToCart(data.Data);
				HttpContext.Session.Set<Cart>("cart", _cart);
			}
			ViewBag.Cart = HttpContext.Session.Get<Cart>("cart");
			return Redirect(returnUrl);
		}

		[Route("[controller]/remove/{id:int}")]
		public ActionResult Remove(int id)
		{
			_cart = HttpContext.Session.Get<Cart>("cart") ?? new();
			_cart.RemoveItems(id);
			HttpContext.Session.Set<Cart>("cart", _cart);
			ViewBag.Cart = HttpContext.Session.Get<Cart>("cart");
			return RedirectToAction("index");
		}
	}
}

