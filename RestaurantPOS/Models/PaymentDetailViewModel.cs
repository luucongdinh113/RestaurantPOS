using System;

namespace RestaurantPOS.Models
{
    public class PaymentDetailViewModel
    {
        public Guid BillId { get; set; }
        public string FoodName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }
}