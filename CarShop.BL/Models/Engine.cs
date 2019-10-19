namespace CarShop.BL.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Horsepower { get; set; }

        /*
        public Engine(string name, int horsepower)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            Name = name;
            Horsepower = horsepower;
        }
        */
    }
}