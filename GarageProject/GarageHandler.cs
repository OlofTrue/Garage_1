using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Garage_1
{
    static public class GarageHandler
    {
        private static Garage<Vehicle> garage;

        public static void SetUpGarage(int cap)
        {
            garage = null;
            garage = new Garage<Vehicle>(cap);
        }

        public static Garage<Vehicle> GetGarageCopyForTest(int cap)
        {
            return new Garage<Vehicle>(cap);
        }

        public static Boolean AddVehicle( Vehicle vehicle)
        {
            var success = false;
            if (garage?.GetVehicle(vehicle.RegNr)==null)
               success= garage?.AddVehicle(vehicle)??false;
            return success;
        }

        public static Boolean RemoveVehicle(string regNr)
        {
            return garage?.RemoveVehicle(regNr)??false;
        }

        public static string ListVehicles(string regNr = "", Boolean onlyParked = true) =>
            (garage is null) ? "" :
            string.Join("\n", garage
                .Where(item => (regNr.Length > 0 ? item?.RegNr == regNr : true)
                            && (item?.IsParked ?? false == onlyParked))
                .ToList()
                .Select(i => i.ToString()));
                    
        public static string ListGarageCapacity()
        {
           return "Capacity: " + garage.Count().ToString()
                     + "\n\nMaxcapacity: " + Garage<Vehicle>.MAX_CAPACITY.ToString();
        }

        public static string ListVehicle(string search)
        { 
            var vehicle= garage?.GetVehicle(search)??null;
            return ListVehicles(vehicle?.RegNr);
        }

        public static void Park(Vehicle vehicle) => vehicle.IsParked = true;
        public static void UnPark(Vehicle vehicle) => vehicle.IsParked = false;

        internal static string StatsVehiclesInGarage()
        {
            if (garage is null) return "";
           var result_list = garage
                .Where(v => v != null && v.IsParked )
                .GroupBy(v => v.Type)
                .Select(group => new {
                    Type = group.Key,
                    Count = group.Count()
                })
                .ToList();
            return string.Join("\n", result_list);
        }

        internal static Vehicle BuildVehicle(string type)
        {
            Console.Write("Specify reg.nr of Vehicle: ");
            var regNr = Console.ReadLine();

            Console.Write("Specify color of Vehicle: ");
            var color = Console.ReadLine();

            switch (type)
            {
                case "Car": return  new Car() { RegNr = regNr, Color = color };

                case "Boat": return new Boat() { RegNr = regNr, Color = color };

                default:
                    break;
            }
            return null;
            //foreach (var item in vehicle?.GetVehicleProperties)
            //{
            //    Console.Write($"Specify {item}: ");
            //    var strValue = Console.ReadLine();
            //    //Boolean isString = (vehicle.GetType().GetProperty(item.ToString()).PropertyType == typeof(string)); int.Parse(strValue)
            //    //vehicle.GetType().GetProperty(item.ToString()).SetValue(vehicle,  strValue, null);

            //    ;
            //    var obj = vehicle;
            //    PropertyInfo prop = obj.GetType().GetProperty(item.ToString(), BindingFlags.Public | BindingFlags.Instance);
            //    if (null != prop && prop.CanWrite)
            //    {
            //        prop.SetValue(obj, strValue, null);
            //    }
            //}



            //public static bool IsStringType(object data)
            //{
            //    return (data.GetType().GetProperties().Where(x => x.PropertyType == typeof(string)).FirstOrDefault() != null);
            //}

        }
    }
}
