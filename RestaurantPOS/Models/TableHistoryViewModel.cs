using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPOS.Models
{
    public class TableHistoryViewModel
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string TableName { get; set; }
        public int PeopleCount { get; set; }
    }
}
