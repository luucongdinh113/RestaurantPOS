using System;
using System.Collections.Generic;

namespace RestaurantPos.Models
{
    public class ListBookInfoViewModel
    {
        public List<BookTableInfoViewModel> listtable { get; set; }
        public DateTime From {  get; set; }
        public DateTime To {  get; set; }
        public int PeopleCount { get; set; }

    }
}
