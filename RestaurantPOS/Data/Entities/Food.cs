using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPOS.Data.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public List<BillDetail> BillDetails { get; set; }
    }
}
