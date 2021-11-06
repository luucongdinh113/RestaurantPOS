using System.Collections.Generic;

namespace RestaurantPOS.Models
{
    public class CartViewModel
    {
        public int Total { get; set; }
        public List<CartDetailViewModel> ListFood { get; set; }
    }
}
