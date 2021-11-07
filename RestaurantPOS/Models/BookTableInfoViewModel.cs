using System;

namespace RestaurantManagement.Models
{
    public class BookTableInfoViewModel
    {
        public DateTime OrderDate {  get; set; }
        public int TableId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int People { get; set; }
    }
}
