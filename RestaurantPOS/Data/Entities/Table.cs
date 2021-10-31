using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPOS.Data.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PeopleCount { get; set; }

        public List<OrderTable> OrderTables { get; set; }
    }
}
