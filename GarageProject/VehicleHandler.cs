using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Garage_1
{
    class VehicleHandler
    {

        public static void Park(Vehicle vehicle) => vehicle.IsParked = true;
        public static void UnPark(Vehicle vehicle) => vehicle.IsParked = false;

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
                    }
                }
            }
            return vehicle;
        }

    }
}
