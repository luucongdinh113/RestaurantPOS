using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPOS.Models
{
    public class InsertFoodViewModel
    {
        [Required()]
        public Guid BillId { get; set; }
        [Required()]
        public int FoodId { get; set; }
        [Required()]
        public int UnitPrice { get; set; }
        [Required()]
        public int Quantity { get; set; }
        [Required()]
        public int Price { get; set; }
    }
}
