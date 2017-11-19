namespace PieFactory
{
    class RobotLucy
    {
        public bool AddFilling(Crust crust, Hopper currentHopper)
        {
            crust.Filling = 250;
            currentHopper.Contain -= 250;
            ThreadWait.ThreadWaitMilisseconds(10);
            return false;
        }

        public void AddFlavour(Crust crust, Hopper currentHopper)
        {
            crust.Flavor = 10;
            currentHopper.Contain -= 10;
            ThreadWait.ThreadWaitMilisseconds(10);
        }

        public void AddTopping(Crust crust, Hopper currentHopper)
        {
            crust.Topping = 100;
            currentHopper.Contain -= 100;
            ThreadWait.ThreadWaitMilisseconds(10);
        }

        public bool EnoughForDispensing(Hopper currentHopper, int quantity)
        {
            return (currentHopper.Contain >= quantity) ? true : false;
        }
    }
}
