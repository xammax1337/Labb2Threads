using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Labb2Threads
{
    public class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int Distance { get; set; }
        public int EventTimer { get; set; }

        public Car(string name, int speed, int distance)
        {
            Name = name;
            Speed = 100; // 100km/h
            Distance = distance;
            EventTimer = 0;

            Task.Run(async () =>
            {
                while (true)
                {
                    CheckCarStatus();
                    await Task.Delay(1000);
                    Distance += Speed;
                    EventTimer++;
                    if (EventTimer == 30) 
                    {
                        await ApplyEvents(this);
                        EventTimer = 0;
                    }
                }
            });
        }

        public void CheckCarStatus()
        {
            Console.WriteLine($"{Name} status: Distance = {Distance}, Speed = {Speed} km/h");
        }

        public static async Task ApplyEvents(Car car)
        {
            Random random = new Random();
            int randomNum = random.Next(1, 51);

            if (randomNum == 1) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                await Console.Out.WriteLineAsync($"!!!!{car.Name} har slut på bensin! Stannar: 30s");
                Console.ForegroundColor = ConsoleColor.White;
                await Task.Delay(30000);
            }
            else if (randomNum <=3 && randomNum != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                await Console.Out.WriteLineAsync($"!!!!{car.Name} fick punktering! Stannar: 20s");
                Console.ForegroundColor = ConsoleColor.White;
                await Task.Delay(20000);
            }
            else if (randomNum <= 8 && randomNum > 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                await Console.Out.WriteLineAsync($"!!!!{car.Name} har en fågel på vindrutan! Stannar: 10s");
                Console.ForegroundColor = ConsoleColor.White;
                await Task.Delay(10000);
            }
            else if (randomNum <= 18 && randomNum > 8)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                await Console.Out.WriteLineAsync($"!!!!{car.Name} har fått motorfel! Sänker hastighet med 1km/h");
                Console.ForegroundColor = ConsoleColor.White;
                car.Speed --;
            }
        }
    }
}
