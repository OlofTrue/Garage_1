﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Garage_1
{
    public static class VehicleHandler
    {
        internal static IVehicle BuildVehicle(string type,out string txtErr) //,string regNr
        {
            txtErr = "";
            Vehicle vehicle = null;
            switch (type.ToLower())
            {
                case "airplane":
                case "ai":
                case "1":
                    vehicle = new Airplane();
                    break;
                case "motorcycle":
                case "mo":
                case "2":
                    vehicle = new Motorcycle();
                    break;
                case "car":
                case "ca":
                case "3":
                    vehicle = new Car();
                    break;
                case "bus":
                case "bu":
                case "4":
                    vehicle = new Bus();
                    break;
                case "boat":
                case "bo":
                case "5":
                    vehicle = new Boat();
                    break;
                default:
                    break;
            }

            if (vehicle != null)
            {
                foreach (var p in vehicle.GetVehicleProperties())
                {
                    var nameP = (string)p?.GetType().GetProperty("NameP")?.GetValue(p, null);
                    if (nameP == "Type") continue;
                    var typeP = (string)p?.GetType().GetProperty("TypeP")?.GetValue(p, null);
                    var label = string.Format($"{nameP}: ");
                    Util.PrintL(); //ToDo Func<> arg
                    string strValue = Util.Input(label); //ToDo Func<> arg
                    if (nameP=="RegNr") //ToDo; unique
                        if (GarageHandler.ListVehicles(strValue).Length>0)
                        {
                            txtErr = "Vehicle already exists.";
                            return null;
                        }
                    var obj = vehicle;
                    PropertyInfo prop = obj.GetType().GetProperty(nameP, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite) //ToDo check prop.GetCustomAttribute for DisplayName
                    {
                        if (typeP == "Int32") prop.SetValue(obj, Util.ConvInt(strValue), null);
                        if (typeP == "String") prop.SetValue(obj, strValue, null);
                        if (typeP == "Single") prop.SetValue(obj, Util.ConvFloat(strValue), null);
                        if (typeP == "Boolean") prop.SetValue(obj, (strValue == "true" || strValue == "1"), null);
                    }
                }
            }
            return vehicle;
        }

        public static Vehicle[] GetTestVehicles() => new Vehicle[]
                    {
                   new Car {RegNr="ABC11",Color="Red",NoWheels=4},
                   new Car {RegNr="ABC222",Color="White",NoWheels=4},
                   new Car {RegNr="ABC333",Color="Grey",NoWheels=4},
                   new Car {RegNr="ABC444",Color="Grey",NoWheels=4},
                   new Boat {RegNr="B-111",Color="Grey",NoWheels=0},
                   new Boat {RegNr="B-222",Color="Blue",NoWheels=0},
                   new Bus {RegNr="PPP999",Color="Grey",NoWheels=6},
                   new Boat {RegNr="QQQ888",Color="Blue",NoWheels=4},
                   new Motorcycle{RegNr="NNN444",Color="Black",NoWheels=2},
                   new Motorcycle{RegNr="MMM555",Color="White",NoWheels=2},
                   new Airplane{RegNr="AS-123-US",Color="Blue",NoWheels=8},
                   new Airplane{RegNr="FF-003-US",Color="Blue",NoWheels=6}
                      };
    }
}