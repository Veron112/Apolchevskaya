using Ski.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Domain.Cart
{
    public class CartItem
    {
        public Skii Item { get; set; }
        public int Qty { get; set; }

    }
}
