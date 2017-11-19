namespace PieFactory
{
    class RobotJoe
    {
        public void FillHopper(Hopper currentHopper, int quantity)
        {
            if (currentHopper.Contain <= (2000 - quantity))
            {
                int numberOfIterations = quantity / 10;

                for (int i = 0; i < numberOfIterations; i++)
                {
                    currentHopper.Contain += 10;
                    ThreadWait.ThreadWaitMilisseconds(1);
                }
            }
        }
    }
}