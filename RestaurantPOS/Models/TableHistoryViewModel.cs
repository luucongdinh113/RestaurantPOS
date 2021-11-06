using System;

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
