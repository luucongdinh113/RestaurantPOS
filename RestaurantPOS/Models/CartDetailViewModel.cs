using System;

namespace RestaurantPOS.Models
{
    public class CartDetailViewModel
    {
        public Guid Id { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
        public string Type { get; set; }
    }
}
