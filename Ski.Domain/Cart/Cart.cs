﻿using Ski.Domain.Cart;
using Ski.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Domain.Cart
{
	public class Cart
	{
		public int Id { get; set; }
		/// <summary>
		/// Список объектов в корзине
		/// key - идентификатор объекта
		/// </summary>
		public Dictionary<int, CartItem> CartItems { get; set; } = new();
		/// <summary>
		/// Добавить объект в корзину
		/// </summary>
		/// <param name="skii">Добавляемый объект</param>
		public virtual void AddToCart(Skii skii)
		{
			if (CartItems.ContainsKey(skii.SkiId))
			{
				CartItems[skii.SkiId].Qty++;
			}
			else
			{
				CartItems.Add(skii.SkiId, new CartItem
				{
					Item = skii,
					Qty = 1
				});
			};
		}
		/// <summary>
		/// Удалить объект из корзины
		/// </summary>
		/// <param name="ski">удаляемый объект</param>
		public virtual void RemoveItems(int id)
		{
			CartItems.Remove(id);
		}
		/// <summary>
		/// Очистить корзину
		/// </summary>
		public virtual void ClearAll()
		{
			CartItems.Clear();
		}
		/// <summary>
		/// Количество объектов в корзине
		/// </summary>
		public int Count { get => CartItems.Sum(item => item.Value.Qty); }
		/// <summary>
		/// Общее количество калорий
		/// </summary>
		/// 
		public double TotalPrice
		{
			get => CartItems.Sum(item => Double.Parse(item.Value.Item.Price) * item.Value.Qty);

		}
	}
}

