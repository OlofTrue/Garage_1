using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Garage_1
{
    public static class VehicleHandler
    {
        internal static Vehicle BuildVehicle(string type) //,string regNr
        {
            Vehicle vehicle = null;
            switch (type.ToLower())
            {
                case "airplane":
                case "ai":
                    vehicle = new Airplane();
                    break;
                case "motorcycle":
                case "mo":
                    vehicle = new Motorcycle();
                    break;
                case "car":
                case "ca":
                    vehicle = new Car();
                    break;
                case "bus":
                case "bu":
                    vehicle = new Bus();
                    break;
                case "boat":
                case "bo":
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

                    var label = string.Format($"Specify {nameP}: ");
                    string strValue = Util.Input(label);

                    var obj = vehicle;
                    PropertyInfo prop = obj.GetType().GetProperty(nameP, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        if (typeP == "Int32") prop.SetValue(obj, Util.ConvInt(strValue), null);
                        if (typeP == "String") prop.SetValue(obj, strValue, null);
                        if (typeP == "Single") prop.SetValue(obj, Util.ConvFloat(strValue), null);
                        if (typeP == "Boolean") prop.SetValue(obj, (strValue == "true" || strValue == "1"), null);


                        //.GetProperty(nameP).GetCustomAttribute<Display>();

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
