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

        private static void FindVehicle() =>Util.MsgBox("Find vehicle", "Not implemented");
        private static void FindVehicleByRegNr()
        {
            Util.PrintClear();
            var regNr = Util.Input("Specify reg.nr of Vehicle: ");
            Util.MsgBox("Find vehicle by regNr",string.Format($"{GarageHandler.ListVehicle(regNr)}"));
        }

        private static void RemoveVehicle()
        {
            Util.PrintClear();
            var regNr = Util.Input("Specify reg.nr of Vehicle: ");
            string str;
            if (GarageHandler.RemoveVehicle(regNr))
                str=string.Format($"Removal of vehicle with Reg.nr:{regNr} was succesfull");
            else
                str = string.Format($"Sorry, removal of vehicle with Reg.nr:{regNr} faild");
            Util.MsgBox("Vehicle removal", str);
        }
        private static void PrintGarageStat() => 
            Util.MsgBox("Garage stat", GarageHandler.StatsVehiclesInGarage()+"\n\n" + GarageHandler.ListGarageCapacity());
        static void PrintGarage() => Util.MsgBox("Garage inv", GarageHandler.ListVehicles());
       
        static void  AddVehicle()
        {
            Util.PrintClear();
            var type =Util.Input("Specify type of Vehicle: ");
            Vehicle vehicle = VehicleHandler.BuildVehicle(type);
            string str;
            if (GarageHandler.AddVehicle(vehicle))
                str=string.Format($"Adding vehicle was succesfull");
            else
                str = string.Format($"Sorry, adding of vehicle faild");
            Util.MsgBox("Add Vehicle", str);
        }

        static void CreateGarage()
        {
            Util.PrintClear();
            var cap = Util.ConvInt( Util.Input("Specify capacity of garage: "));
            GarageHandler.SetUpGarage(cap);
            Util.MsgBox("New garage", string.Format($"{GarageHandler.ListVehicles()}\n\n{GarageHandler.ListGarageCapacity()}"));
        }
      
        static void ImportVehicles()
        {
            var cnt = 0;
            var vehicles = GarageHandler.GetTestVehicles();
            foreach (var v in vehicles)
            {
                if (GarageHandler.AddVehicle(v)) cnt++;
            }
            Util.MsgBox("Import", string.Format($"Succesfully imported {cnt} (of {vehicles.Length}) vehicles"));

        }
    }
}







