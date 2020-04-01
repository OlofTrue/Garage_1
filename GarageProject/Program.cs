using System;
using System.Collections.Generic;

namespace Garage_1
{
    class Program
    {
        static Garage<Vehicle> theGarage;
        static void Main() //string[] args
        {
            var cap= Util.InputPositivInt("Specify capacity of Garage: ");
            theGarage = GarageHandler.SetUpGarage(cap);
            while (MainMenu());           
        }
        

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Garage manager\n"
                + "\n1. Examine garage"
                + "\n2. Statistics garage"
                + "\n3. Add Vehicle"
                + "\n4. Remove Vehicle"
                + "\n5. aaaa"
                + "\n9. Import some veicle"
                + "\n0. Exit the application");
            Console.Write("\r\nSelect an option: ");
            var actionMeny = new Dictionary<string, Action>()
            {
                {"D1", PrintGarage },
                {"D2", PrintGarageStat },
                {"D3", AddVehicle },
                {"D4", RemoveVehicle },
                {"D5", Foo },
                {"D9", ImportVehicles },
                {"D0", ()=>{Environment.Exit(0); } }
            };
            var strMenu =Console.ReadKey(intercept: true).Key.ToString();
            if (actionMeny.ContainsKey(strMenu)) actionMeny[strMenu]?.Invoke();
            return true;
        }

        private static void RemoveVehicle()
        {
            throw new NotImplementedException();
        }

        private static void PrintGarageStat()
        {
            Console.Clear();
            Console.WriteLine("------------ Garage stat ------------");
            Console.WriteLine();
            Console.WriteLine($"{GarageHandler.StatsVehicles(theGarage)}");
            Console.WriteLine();
            Console.WriteLine("------------ Garage stat ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        static void PrintGarage()
        {
            Console.Clear();
            Console.WriteLine("------------ Garage inv ------------");
            Console.WriteLine();
            Console.WriteLine($"{GarageHandler.ListVehicles(theGarage)}");
            Console.WriteLine();
            Console.WriteLine("------------ Garage inv ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        static void  AddVehicle()
        {
            //throw new NotImplementedException();
        }

        static void ImportVehicles()
        {
            var testCars= new Vehicle[]
                        {
                   new Car {RegNr="ABC11",Color="Red",NoWheels=4},
                   new Car {RegNr="ABC222",Color="White",NoWheels=4},
                   new Car {RegNr="ABC333",Color="Grey",NoWheels=4},
                   new Car {RegNr="ABC444",Color="Grey",NoWheels=4},
                   new Boat {RegNr="B-111",Color="Grey",NoWheels=0},
                   new Boat {RegNr="B-222",Color="Blue",NoWheels=0}
                        };
            foreach (var car in testCars)
            {
                GarageHandler.AddVehicle(theGarage,car);
            }

            Console.Clear();
            Console.WriteLine("------------ Import ------------");
            Console.WriteLine();
            Console.WriteLine($"Imported {testCars.Length} vehicles");
            Console.WriteLine();
            Console.WriteLine("------------ Import ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        static void Foo()
        {
            //throw new NotImplementedException();
        }
    }
}







