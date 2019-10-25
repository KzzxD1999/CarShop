namespace CarShop.BL.Models
{
    public class BasketCar
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public bool InBasket { get; set; } = false;
        public int Count { get; set; } = 1;
    }
}