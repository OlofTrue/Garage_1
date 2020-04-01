using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Garage_1
{
    static public class GarageHandler
    {

        public static Garage<Vehicle> SetUpGarage(int cap)
        {
            return new Garage<Vehicle>(cap);
        }

        public static Boolean AddVehicle(Garage<Vehicle> garage, Vehicle vehicle)
        {
            return garage.AddVehicle(vehicle);
        }

        public static Boolean RemoveVehicle(Garage<Vehicle> garage, string regNr)
        {
            return garage.RemoveVehicle(regNr);
        }

        public static string ListVehicles(Garage<Vehicle> garage, Boolean onlyParked = true) => 
            string.Join("\n", garage
                .Where(item => item?.IsParked ?? false)
                .ToList()
                .Select(i => i.ToString()));
      


        public static Vehicle GetVehicle(Garage<Vehicle> garage, string search)
        {

            return garage.GetVehicle(search);
            //throw new NotImplementedException();
        }

        public static void Park(Vehicle vehicle) => vehicle.IsParked = true;
        public static void UnPark(Vehicle vehicle) => vehicle.IsParked = false;

        internal static object StatsVehicles(Garage<Vehicle> theGarage)
        {
            throw new NotImplementedException();
        }
    }
}
