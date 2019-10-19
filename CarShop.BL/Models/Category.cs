using System.Collections.Generic;

namespace CarShop.BL.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
       // public bool State { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    
}