using System;
using System.Diagnostics;

namespace RobotVS
{
    class Program
    {
        static void Main(string[] args)


        {

            Console.WriteLine("Do you want to wait for debuger? write y or n");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "y") { 
            for (; ; )
            {
                Console.WriteLine("Waiting for debugger attach");
                if (Debugger.IsAttached) break;
                System.Threading.Thread.Sleep(1000);
            }
            }
            Console.WriteLine("Press 1 for ultra sonic OR 2 for Motor or 3 for moving");

            string typed = Console.ReadLine();
            Moving moving = new Moving();
            while (typed == "1" || typed == "2" || typed == "3")
            {

                if (typed == "1")
                {
                    Sonic sonic = new Sonic();
                    
                    sonic.Start();

                }
                else if (typed == "2")
                {
                    Motor motor = new Motor();
                    motor.Start();

                }
                else if (typed == "3")
                {
                    moving.notStop = true;
                    moving.Run();



                }
                moving.EndAll();
                Console.WriteLine("Press 1 for ultra sonic OR 2 for Motor or 3 for moving");

                typed = Console.ReadLine();

            }

        }
    }
}
