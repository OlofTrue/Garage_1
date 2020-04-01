using System;
using System.Collections.Generic;

namespace Garage_1
{
    class Program
    {
        static Garage<Vehicle> theGarage;
        static void Main() //string[] args
        {
            var cap= Util.InputPositivInt("Specify capacity");
            theGarage = GarageHandler.SetUpGarage(cap);
            while (MainMenu());           
        }
        

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Garage manager\n"
                + "\n1. Examine garage"
                + "\n2. Add Vehicle"
                + "\n3. aaaa"
                + "\n4. bbb"
                + "\n0. Exit the application");
            Console.Write("\r\nSelect an option: ");
            var actionMeny = new Dictionary<string, Action>()
            {
                {"D1", Foo },
                {"D2", AddVehicle },
                {"D3", Foo },
                {"D9", FillUpGarage },
                {"D0", ()=>{Environment.Exit(0); } }
            };
            var strMenu =Console.ReadKey(intercept: true).Key.ToString();
            if (actionMeny.ContainsKey(strMenu)) actionMeny[strMenu]?.Invoke();
            return true;
        }

        static void  AddVehicle()
        {
            //throw new NotImplementedException();
        }

        static void FillUpGarage()
        {
            var testCars= new Vehicle[]
                        {
                   new Car {RegNr="ABC11",Color="Red",NoWheels=4},
                   new Car {RegNr="ABC222",Color="White",NoWheels=4},
                   new Car {RegNr="ABC333",Color="Grey",NoWheels=4}
                        };
            foreach (var car in testCars)
            {
                GarageHandler.AddVehicle(theGarage,car);
            }
        }

        static void Foo()
        {
            //throw new NotImplementedException();
        }
    }
}







