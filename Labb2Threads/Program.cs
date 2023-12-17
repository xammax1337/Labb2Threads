using System;
using System.Threading.Tasks;

namespace Labb2Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Car car1 = new Car("Audi", 100, 0);
            Car car2 = new Car("Tesla", 100, 0);

            Task car1Task = RunCar(car1);
            Task car2Task = RunCar(car2);

            await Task.WhenAll(car1Task, car2Task);

            if (car1.Distance > car2.Distance)
            {
                Console.Out.WriteLineAsync($"{car1.Name} won");
            }
            else if (car1.Distance < car2.Distance)
            {
                Console.Out.WriteLineAsync($"{car2.Name} won");
            }
        }

        static async Task RunCar(Car car)
        {
            int finishSpeed = 0;
            int raceDistance = 10000;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{car.Name} has started driving");
            bool raceActive = true;

            while (raceActive)
            {
                finishSpeed++;

                await Task.Delay(1000);

                CheckEnterKey(car);

                if (car.Distance >= raceDistance)
                {
                    raceActive = false;
                    Console.WriteLine($"{car.Name} Has finished the race in {finishSpeed} Seconds");
                }
            }
            
        }

        static void CheckEnterKey(Car car)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    car.CheckCarStatus();
                }
            }
        }
    }
}
