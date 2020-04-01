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
            garage= new Garage<Vehicle>(cap);
        }

        public static Garage<Vehicle> GetGarageCopyForTest(int cap)
        {
            return new Garage<Vehicle>(cap);
        }

        public static Boolean AddVehicle( Vehicle vehicle)
        {
            return garage.AddVehicle(vehicle);
        }

        public static Boolean RemoveVehicle(string regNr)
        {
            return garage.RemoveVehicle(regNr);
        }

        public static string ListVehicles(Boolean onlyParked = true) => 
            string.Join("\n", garage
                .Where(item => item?.IsParked ?? false == onlyParked)
                .ToList()
                .Select(i => i.ToString())) 
                    + "\n\n Capacity: " + garage.Count().ToString()
                     + "\n\n ( Maxcapacity: " + Garage<Vehicle>.MAX_CAPACITY.ToString() +" )";
        public static Vehicle GetVehicle(string search)
        { 

            return garage.GetVehicle(search);
            //throw new NotImplementedException();
        }

        public static void Park(Vehicle vehicle) => vehicle.IsParked = true;
        public static void UnPark(Vehicle vehicle) => vehicle.IsParked = false;

        internal static string StatsVehiclesInGarage()
        {
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
