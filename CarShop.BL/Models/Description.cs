using System.Collections.Generic;

namespace CarShop.BL.Models
{
    public class Description
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public bool IsShown { get; set; }

    }
}