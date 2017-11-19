namespace PieFactory
{
    class Crust
    {
        public int Filling { get; set; }
        public int Flavor { get; set; }
        public int Topping { get; set; }

        public Crust()
        {
            Filling = 0;
            Flavor = 0;
            Topping = 0;
        }
    }
}