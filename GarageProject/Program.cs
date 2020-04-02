using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Garage_1
{
    class Program
    {
        
        static void Main() //string[] args
        {
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
                + "\n5. Nytt garage"
                + "\n6. Find vehicle by regnr"
                + "\n7. Find vehicle (generic)"
                + "\n9. Import some Vehicles"
                + "\n0. Exit the application");
            Console.Write("\r\nSelect an option: ");
            var actionMeny = new Dictionary<string, Action>()
            {
                {"D1", PrintGarage },
                {"D2", PrintGarageStat },
                {"D3", AddVehicle },
                {"D4", RemoveVehicle },
                {"D5", CreateGarage },
                {"D6", FindVehicleByRegNr },
                {"D7", FindVehicle },
                {"D9", ImportVehicles },
                {"D0", ()=>{Environment.Exit(0); } }
            };
            var strMenu =Console.ReadKey(intercept: true).Key.ToString();
            if (actionMeny.ContainsKey(strMenu)) actionMeny[strMenu]?.Invoke();
            return true;
        }

        private static void FindVehicle()
        {

            Console.Clear();
            Console.WriteLine("------------ Find vehicle ------------");
            Console.WriteLine();
            Console.WriteLine("Not implemented");
            Console.WriteLine();
            Console.WriteLine("------------ Find vehicle ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
            //throw new NotImplementedException();
        }

        private static void FindVehicleByRegNr()
        {
            Console.Clear();
            Console.Write("Specify reg.nr of Vehicle: ");
            var regNr = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("------------ Find vehicle by regNr ------------");
            Console.WriteLine();
            Console.WriteLine($"{GarageHandler.ListVehicle(regNr)}");
            Console.WriteLine();
            Console.WriteLine("------------ Find vehicle by regNr ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        private static void RemoveVehicle()
        {
            Console.Clear();
            Console.Write("Specify reg.nr of Vehicle: ");
            var regNr = Console.ReadLine();
            
            Console.Clear();
            Console.WriteLine("------------ Vehicle removal ------------");
            Console.WriteLine();
            if (GarageHandler.RemoveVehicle(regNr)) 
                Console.WriteLine($"Removal of vehicle with Reg.nr:{regNr} was succesfull");
            else 
                Console.WriteLine($"Sorry, removal of vehicle with Reg.nr:{regNr} faild");
            Console.WriteLine();
            Console.WriteLine("------------ Vehicle removal ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        private static void PrintGarageStat()
        {
            Console.Clear();
            Console.WriteLine("------------ Garage stat ------------");
            Console.WriteLine();
            Console.WriteLine($"{GarageHandler.StatsVehiclesInGarage()}\n\n{GarageHandler.ListGarageCapacity()}");
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
            Console.WriteLine($"{GarageHandler.ListVehicles()}");
            Console.WriteLine();
            Console.WriteLine("------------ Garage inv ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
        }

        static void  AddVehicle()
        {
            Console.Clear();
            Console.WriteLine("------------ Add Vehicle ------------");
            Console.WriteLine();
            Console.Write("Specify type of Vehicle: ");
            var type = Console.ReadLine();
            //Console.Write("Specify reg.nr of Vehicle: ");
            //var regNr = Console.ReadLine();
            Vehicle vehicle = GarageHandler.BuildVehicle(type); //, regNr);

            Console.WriteLine("------------ Add Vehicle ------------");
            Console.WriteLine();
            if (GarageHandler.AddVehicle(vehicle))
                Console.WriteLine($"Adding vehicle was succesfull");
            else
                Console.WriteLine($"Sorry, adding of vehicle faild");
            Console.WriteLine();
            Console.WriteLine("------------ Add Vehicle ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();

        }

        static void CreateGarage()
        {
            Console.Clear();
            Console.Write("Specify capacity of Garage: ");
            int cap = int.TryParse(Console.ReadLine(), out cap) ? cap : -1;

            GarageHandler.SetUpGarage(cap);
            Console.WriteLine("------------ Garage inv ------------");
            Console.WriteLine();
            Console.WriteLine($"{GarageHandler.ListVehicles()}\n\n{GarageHandler.ListGarageCapacity()}");
            Console.WriteLine();
            Console.WriteLine("------------ Garage inv ------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return: ");
            Console.ReadLine();
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
                GarageHandler.AddVehicle(car);
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
    }
}







