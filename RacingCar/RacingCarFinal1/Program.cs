﻿using System.Threading.Tasks;
using System.Xml.Linq;

namespace RacingCarFinal1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Reference my CARRACING FILE.
            Console.WriteLine("Car Racing will start when you press ENTER");
            Console.ReadKey();

            Car firstCar = new Car
            {
                Id = 1,
                CarName = "Lamborghini",
                DistanceStart = 0,
                SpeedPerHour = 120, // per hour
                SpeedPerSecond = 33.34M, // per second
                DistanceEnd = 10000, // 1 km = 1000 meter, So, 10*1000 = 10000 
            };

            Car secondCar = new Car
            {
                Id = 2,
                CarName = "Rolls Royce",
                DistanceStart = 0,
                SpeedPerHour = 120, // per hour
                SpeedPerSecond = 33.34M, // per second
                DistanceEnd = 10000, // 1 km = 1000 meter, So, 10*1000 = 10000 
            };

            Car thirdCar = new Car
            {
                Id = 3,
                CarName = "Porsche 911",
                DistanceStart = 0,
                SpeedPerHour = 120, // per hour
                SpeedPerSecond = 33.34M, // per second
                DistanceEnd = 10000, // 1 km = 1000 meter, So, 10*1000 = 10000 
            };

            Car fourthCar = new Car
            {
                Id = 4,
                CarName = "Ferrari",
                DistanceStart = 0,
                SpeedPerHour = 120, // per hour
                SpeedPerSecond = 33.34M, // per second
                DistanceEnd = 10000, // 1 km = 1000 meter, So, 10*1000 = 10000 
            };

            //var firstCarTask = OutOfGas(firstCar);
            //var secondCarTask = Punture(secondCar);
            //var thirdCarTask = Windshield(thirdCar);
            //var fourthCarTask = EngineFailure(fourthCar);


            var firstCarTask = GenerateEvent(firstCar);
            var secondCarTask = GenerateEvent(secondCar);
            var thirdCarTask = GenerateEvent(thirdCar);
            var fourthCarTask = GenerateEvent(fourthCar);

            var statusCarTask = CarRacingStatus(new List<Car> { firstCar, secondCar, thirdCar, fourthCar });

            var carTasks = new List<Task> { firstCarTask, secondCarTask, thirdCarTask, fourthCarTask };

            while (carTasks.Count > 0)
            {

                Task finishedTask = await Task.WhenAny(carTasks);

                if (finishedTask == firstCarTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n     Lamborghini has Completed the Race. CONGRATULATION \n");
                    Console.ResetColor();
                    Console.ReadKey();
                    //PrintCar(firstCar);
                }
                else if (finishedTask == secondCarTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n     Rolls Royce has Completed the Race. CONGRATULATION \n");
                    Console.ResetColor();
                    //PrintCar(secondCar);
                }

                else if (finishedTask == thirdCarTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n     Porsche 911 has Completed the Race. CONGRATULATION \n");
                    Console.ResetColor();
                    //PrintCar(thirdCar);
                }

                else if (finishedTask == fourthCarTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n     Ferrari has Completed the Race. CONGRATULATION \n");
                    Console.ResetColor();
                    //PrintCar(fourthCar);
                }

                else if (finishedTask == fourthCarTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n     Simulation over       \n");
                    Console.ResetColor();
                }

                await finishedTask;
                carTasks.Remove(finishedTask);
            }
        }

        public async static Task<Car> OutOfGas(Car car)
        {
            int raceDistance = 0; // (10km * 1000) = 10000 m

            while (true)
            {
                await IncidentPeriodWait(3);
                Console.WriteLine($"{car.CarName} is out of gas and needs to refuel");
                await OutofGasWait(3);

                car.DistanceStart += (1000 + raceDistance);

                Console.WriteLine($"{car.CarName} has reach {car.DistanceStart} meter now.");

                if (car.DistanceStart >= car.DistanceEnd)
                {
                    // Car reached.
                    return car;
                }
            }
        }

        public async static Task<Car> Punture(Car car)
        {
            int raceDistance = 0; // (10km * 1000) = 10000 m

            while (true)
            {
                await IncidentPeriodWait(3);
                Console.WriteLine($"{car.CarName} has a puncture and needs to change tires");
                await PunctureWait(2);

                // 10000 m need to reach 5 minutes or 300 second.
                // 1 second need to 33.34 meters
                // 30 second need to (33.34 * 30) = 1000 meter
                // 30 second mean any happened will be occured after 30 seconds.

                car.DistanceStart += (1000 + raceDistance);

                Console.WriteLine($"{car.CarName} has reach {car.DistanceStart} meter now.");

                if (car.DistanceStart >= car.DistanceEnd)
                {
                    // Car reached.
                    return car;
                }
            }
        }

        public async static Task<Car> Windshield(Car car)
        {
            int raceDistance = 0; // (10km * 1000) = 10000 m

            while (true)
            {
                await IncidentPeriodWait(3);
                Console.WriteLine($"{car.CarName} has a Bird Fall. Need to fix Windshield.");
                await BirdWait(1);

                // 10000 m need to reach 5 minutes or 300 second.
                // 1 second need to 33.34 meters
                // 30 second need to (33.34 * 30) = 1000 meter
                // 30 second mean any happened will be occured after 30 seconds.

                car.DistanceStart += (1000 + raceDistance);

                Console.WriteLine($"{car.CarName} has reach {car.DistanceStart} meter now.");

                if (car.DistanceStart >= car.DistanceEnd)
                {
                    // Car reached.
                    return car;
                }
            }

        }

        public async static Task<Car> EngineFailure(Car car) // Problem about minimized the speed
        {
            int raceDistance = 0; // (10km * 1000) = 10000 m
            int thirtySecondNeedtoReach = 1000;
            int engineFailurePerHappened = 1;

            while (true)
            {
                await IncidentPeriodWait(3);
                Console.WriteLine($"{car.CarName} has an engine failure and needs to reduce speed.");
                await EngineFailureWait(3);

                // 10000 m need to reach 5 minutes or 300 second.
                // 1 second need to 33.34 meters
                // 30 second need to (33.34 * 30) = 1000 meter
                // 30 second mean any happened will be occured after 30 seconds.


                car.DistanceStart += (thirtySecondNeedtoReach + raceDistance);
                car.SpeedPerHour -= engineFailurePerHappened;

                Console.WriteLine($"{car.CarName} has reach {car.DistanceStart} meter now.");
                Console.WriteLine($"{car.CarName} has reduced the speed due Engine Failur. Now speed is {car.SpeedPerHour}");

                if (car.DistanceStart >= car.DistanceEnd)
                {
                    // Car reached.
                    return car;
                }
            }

        }

        public async static Task IncidentPeriodWait(int delay = 1) // Waitng time after effect.
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine("\n30 Seconds wait Completed\n");
        }

        public async static Task OutofGasWait(int delay = 30) // Out of Gas
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine("\n30 Seconds wait Completed\n");
        }

        public async static Task PunctureWait(int delay = 20) // Tire Puncture 
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine("\n20 Seconds wait Completed\n");
        }

        public async static Task BirdWait(int delay = 10) // Bird on the windshild
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine("\n10 Seconds wait Completed\n");
        }

        public async static Task EngineFailureWait(int delay = 3) // Engine Failure
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine("\n3 Seconds wait Completed\n");

            //int speedreducedPerSeconds = (((60 * 10) / 119) * 60) - (((60 * 10) / 120) * 60);
            //speedreducedPerSeconds = (((60 * 10) / 118) * 60) - (((60 * 10) / 119) * 60);

            // Calculated until 21 time engine failure on average engine waiting time is 3 minutes so 
        }

        public static void PrintCar(Car car)
        {
            Console.WriteLine($"\n{car.CarName} has been racing and speed per hour {car.SpeedPerHour}\n");
        }

        public async static Task CarRacingStatus(List<Car> cars)
        {
            while (true)
            {
                DateTime start = DateTime.Now;

                bool gotKey = false;

                while ((DateTime.Now - start).TotalSeconds < 2)
                {
                    if (Console.KeyAvailable)
                    {
                        gotKey = true;
                        break;
                    }
                }
                if (gotKey)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    //Console.Clear();
                    cars.ForEach(car =>
                    {
                        Console.WriteLine($"\n{car.DistanceStart} meter has reached. Destination 10000 Meters.");
                        Console.WriteLine($"{car.CarName} has speed {car.SpeedPerHour}\n");
                    });

                }
            }
        }

        public async static Task<Car> GenerateEvent(Car car)
        {
            
            int raceDistance = 0; // (10km * 1000) = 10000 m
            int thirtySecondNeedtoReach = 0;
            int engineFailurePerHappened = 1;

            while (true)
            {
                Random random = new Random();

                int eventChance = random.Next(1, 51);

                if (eventChance == 1)
                {
                    // Out of gas
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{car.CarName} is out of gas and needs to refuel.\n");
                    Console.ResetColor();
                    await Task.Delay(TimeSpan.FromSeconds(3));

                    car.DistanceStart += (1000 + raceDistance);

                    Console.WriteLine($"\n{car.CarName} has reach {car.DistanceStart} meter now.\n");

                    if (car.DistanceStart >= car.DistanceEnd)
                    {
                        // Car reached.
                        return car;
                    }
                }

                else if (eventChance >= 2 && eventChance <= 3)
                {
                    // Puncture
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{car.CarName} has a puncture and needs to change tires.\n");
                    Console.ResetColor();
                    await Task.Delay(TimeSpan.FromSeconds(2));

                    car.DistanceStart += (1000 + raceDistance);
                    Console.WriteLine($"\n{car.CarName} has reach {car.DistanceStart} meter now.\n");

                    if (car.DistanceStart >= car.DistanceEnd)
                    {
                        // Car reached.
                        return car;
                    }

                }

                else if (eventChance >= 4 && eventChance <= 8)
                {
                    // Bird on windshield
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{car.CarName} has a bird on the windshield and needs to wash it.\n");
                    Console.ResetColor();
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    car.DistanceStart += (1000 + raceDistance);
                    Console.WriteLine($"\n{car.CarName} has reach {car.DistanceStart} meter now.\n");

                    if (car.DistanceStart >= car.DistanceEnd)
                    {
                        // Car reached.
                        return car;
                    }

                }
                else if (eventChance >= 9 && eventChance <= 18)
                {
                    // Engine failure
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{car.CarName} has an engine failure and needs to reduce speed.\n");
                    Console.ResetColor();
                    await Task.Delay(TimeSpan.FromSeconds(3));

                    car.DistanceStart += (1000 + raceDistance);
                    Console.WriteLine($"\n{car.CarName} has reach {car.DistanceStart} meter now.\n");

                    car.DistanceStart += (thirtySecondNeedtoReach + raceDistance);
                    car.SpeedPerHour -= engineFailurePerHappened;

                    Console.WriteLine($"{car.CarName} has reduced the speed due Engine Failur. Now speed is {car.SpeedPerHour}\n");

                    if (car.DistanceStart >= car.DistanceEnd)
                    {
                        // Car reached.
                        return car;
                    }
                }
            }   
        }
    }
}