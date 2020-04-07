using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Garage_1
{
    class Menu
    {
        internal struct MenuItem
        {
            internal MenuItem(string caption, Action act)
            {
                Caption = caption;
                Act = act;
            }
            internal readonly string Caption;
            internal readonly Action Act;
        };
        internal static bool MainMenu()
        {

            Console.Clear();
            Console.WriteLine("Garage manager\n");
            var actionMeny = new Dictionary<string, MenuItem>()
            {
                { "D1",new MenuItem("New garage", CreateGarage) },
                { "D2",new MenuItem("Examine garage", PrintGarage)},
                { "D3",new MenuItem("Statistics garage", PrintGarageStat) },
                { "D4",new MenuItem("Add Vehicle", AddVehicle) },
                { "D5",new MenuItem("Remove Vehicle", RemoveVehicle) },
                { "D6",new MenuItem("Find vehicle by regnr", FindVehicleByRegNr) },
                { "D7",new MenuItem("Find vehicle (generic any match)", FindVehicle) },
                { "D9",new MenuItem("Create some test-Vehicles", CreateTestVehicles) },
                { "I",new MenuItem("Import some Vehicles", ImportVehicles) },
                { "E",new MenuItem("Export some Vehicles", ExportVehicles) },
                { "D0",new MenuItem("Exit the application", () => { Environment.Exit(0); }) }
             };
            foreach (var item in actionMeny)
            {
                var menuItem = (MenuItem)item.Value;
                Console.WriteLine($"{item.Key.ToString().Replace("D", "")}. {menuItem.Caption}");
                if (GarageHandler.GarageMissing()) Console.ForegroundColor = System.ConsoleColor.DarkGray;
            }
            Console.ForegroundColor = System.ConsoleColor.White;
            Console.Write("\r\nSelect an option: ");
            var strMenu = Console.ReadKey(intercept: true).Key.ToString();
            if (actionMeny.ContainsKey(strMenu)) actionMeny[strMenu].Act.Invoke();
            return true;
        }

        private static void FindVehicle() //=> Util.MsgBox("Find vehicle", "Not implemented");
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            var search = Util.Input("Specify search string ( any of \"4,Red\" or more specific any of  \"NoWheels=4;Color=Red\" ): ");
            Util.MsgBox("Find vehicles by search string", string.Format($"{GarageHandler.ListVehicleG(search)}"));
        }


private static void FindVehicleByRegNr()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            var regNr = Util.Input("Specify reg.nr of Vehicle: ");
            Util.MsgBox("Find vehicle by regNr", string.Format($"{GarageHandler.ListVehicle(regNr)}"));
        }

        private static void RemoveVehicle()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            var regNr = Util.Input("Specify reg.nr of Vehicle: ");
            string str;
            if (GarageHandler.RemoveVehicle(regNr))
                str = string.Format($"Removal of vehicle with Reg.nr:{regNr} was succesfull");
            else
                str = string.Format($"Sorry, removal of vehicle with Reg.nr:{regNr} faild");
            Util.MsgBox("Vehicle removal", str);
        }
        private static void PrintGarageStat() =>
            Util.MsgBox("Garage stat", GarageHandler.StatsVehiclesInGarage() + "\n\n" + GarageHandler.ListGarageCapacity());
        static void PrintGarage()
        {
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            Util.MsgBox("Garage inv", GarageHandler.ListVehicles());
        }
        static void AddVehicle()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            if (GarageHandler.GarageIsFull())
            {
                Util.MsgBox("Message", "Garage is full");
                return;
            }
            var type = Util.Input("Specify type of Vehicle (ai,mo,ca,bu,bo): ");
            Vehicle vehicle = VehicleHandler.BuildVehicle(type);
            string str;
            if (GarageHandler.AddVehicle(vehicle, out var errMsg))
                str = string.Format($"Adding vehicle was succesfull");
            else
                str = string.Format($"Sorry, adding of vehicle faild. {errMsg}");
            Util.MsgBox("Add Vehicle", str);
        }

        static void CreateGarage()
        {
            Util.PrintClear();
            var cap = Util.ConvInt(Util.Input("Specify capacity of garage: "));
            GarageHandler.SetUpGarage(cap);
            Util.MsgBox("New garage", string.Format($"{GarageHandler.ListVehicles()}\n\n{GarageHandler.ListGarageCapacity()}"));
        }

        static void CreateTestVehicles()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            if (GarageHandler.GarageIsFull())
            {
                Util.MsgBox("Message", "Garage is full");
                return;
            }

            var cnt = 0;
            string errLst = "";
            var vehicles = GarageHandler.GetTestVehicles();
            foreach (var v in vehicles)
            {
                if (GarageHandler.AddVehicle(v, out var err)) cnt++;
                else errLst += "\n" + err;
            }
            Util.MsgBox("Create test vehicles", string.Format($"Succesfully created {cnt} (of {vehicles.Length}) vehicles. {errLst}"));
        }


        static void ExportVehicles()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            GarageHandler.Export();

            Util.MsgBox("Message", "Vehicles in garage exported");
        }




        static void ImportVehicles()
        {
            Util.PrintClear();
            if (GarageHandler.GarageMissing())
            {
                Util.MsgBox("Message", "Garage is missing");
                return;
            }
            if (GarageHandler.GarageIsFull())
            {
                Util.MsgBox("Message", "Garage is full");
                return;
            }

            var cnt = 0;
            string errLst = "";
            var vehicles = GarageHandler.Import();
            foreach (var v in vehicles)
            {
                if (GarageHandler.AddVehicle(v, out var err)) cnt++;
                else errLst += "\n" + err;
            }
            Util.MsgBox("Import", string.Format($"Succesfully imported {cnt} (of {vehicles.Count}) vehicles. {errLst}"));
        }

    }
}
