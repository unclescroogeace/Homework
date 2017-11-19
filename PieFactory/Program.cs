using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace PieFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console settings
            Console.Title = "Pie Factory";

            //Initializing variables
            Hopper hopperFilling = new Hopper();
            Hopper hopperFlavor = new Hopper();
            Hopper hopperTopping = new Hopper();
            RobotJoe joe = new RobotJoe();
            RobotLucy lucy = new RobotLucy();
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            AutoResetEvent autoResetEvent2 = new AutoResetEvent(false);
            ConcurrentBag<Crust> crusts = new ConcurrentBag<Crust>();
            int countOfPies = 0;
            Stopwatch stopWatch = new Stopwatch();
            bool isDispensing = false;
            bool isWorking = true;
            bool stopTheFactory = false;
            bool isStopped = false;
            bool isStoppedSuccessfully = false;
            string startingTheFactory = "STARTING THE FACTORY";
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press \"ENTER\" to start the factory and press \"ESCAPE\" to stop it.");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)) ;

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(0, 1);
                startingTheFactory += '.';
                Console.WriteLine(startingTheFactory);
                ThreadWait.ThreadWaitMilisseconds(450);
            }
            Console.WriteLine();

            //Workers
            Thread workerOne = new Thread(new ThreadStart(ConveyorBelt));
            Thread workerTwo = new Thread(new ThreadStart(RobotLucy));
            Thread workerThree = new Thread(new ThreadStart(RobotJoe));

            //Robot Joe is starting...
            workerThree.Start();

            //Conveyor belt is starting...
            workerOne.Start();

            //Robot Lucy is starting...
            workerTwo.Start();

            void ConveyorBelt()
            {
                while (isWorking)
                {
                    stopWatch.Start();

                    //Create a new crust
                    Crust newCrust = new Crust();
                    crusts.Add(newCrust);
                    countOfPies += 1;

                    autoResetEvent.Set();

                    //The speed of the conveyor belt
                    ThreadWait.ThreadWaitMilisseconds(50);

                    //Wait for Robot Lucy if still is dispensing
                    if (isDispensing)
                    {
                        autoResetEvent2.WaitOne();
                    }

                    stopWatch.Stop();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Pie was created successfully!");
                    Console.WriteLine("Time taken for the pie to be created was {0}." + Environment.NewLine, stopWatch.ElapsedMilliseconds);
                    stopWatch.Reset();

                    if (stopTheFactory == true)
                    {
                        isWorking = false;
                        isStopped = true;
                    }
                }
            }

            void RobotLucy()
            {
                while (isWorking)
                {
                    //Waiting for a new crust to be created
                    autoResetEvent.WaitOne();
                    isDispensing = true;

                    //Wait for enough filling and then fill the crust
                    while (!lucy.EnoughForDispensing(hopperFilling, 250))
                    {
                        ThreadWait.ThreadWaitMilisseconds(1);
                    }
                    lucy.AddFilling(crusts.ElementAt(countOfPies - 1), hopperFilling);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Filling was added successfully.");

                    //Wait for enough flavor and then fill the crust
                    while (!lucy.EnoughForDispensing(hopperFlavor, 10))
                    {
                        ThreadWait.ThreadWaitMilisseconds(1);
                    }
                    lucy.AddFlavour(crusts.ElementAt(countOfPies - 1), hopperFlavor);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Flavor was added successfully.");

                    //Wait for enough topping and then fill the crust
                    while (!lucy.EnoughForDispensing(hopperTopping, 100))
                    {
                        ThreadWait.ThreadWaitMilisseconds(1);
                    }
                    lucy.AddTopping(crusts.ElementAt(countOfPies - 1), hopperTopping);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Topping was added successfully.");

                    isDispensing = false;
                    autoResetEvent2.Set();
                }
            }

            void RobotJoe()
            {
                while (isWorking)
                {

                    //Robot Joe fill the filling hopper
                    joe.FillHopper(hopperFilling, 250);

                    //Robot Joe fill the flavor hopper
                    joe.FillHopper(hopperFlavor, 20);

                    //Robot Joe fill the filling hopper
                    joe.FillHopper(hopperFilling, 70);

                    //Robot Joe fill the topping hopper
                    joe.FillHopper(hopperTopping, 130);
                }
            }

            //Stopping the factory when "ESCAPE" is pressed
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) ;
            stopTheFactory = true;
            do
            {
                if (isStopped)
                {
                    isStoppedSuccessfully = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pie factory stopped working successfully!");
                }
            } while (!isStoppedSuccessfully);
        }
    }
}