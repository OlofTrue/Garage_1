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
            if (cap>0) garage = new Garage<Vehicle>(cap);
        }

        public static Garage<Vehicle> GetGarageCopyForTest(int cap)
        {
            SetUpGarage(cap);
            return garage;
        }

        public static bool AddVehicle(Vehicle vehicle, out string errMsg)
        {
            errMsg = "";
            var success = false;
            if (garage?.GetVehicle(vehicle?.RegNr) == null)
                success = garage?.AddVehicle(vehicle) ?? false;
            else
                errMsg = "Reg.nr already exists";
            return success;
        }

        public static Boolean RemoveVehicle(string regNr)
        {
            return garage?.RemoveVehicle(regNr) ?? false;
        }

        public static string ListVehicles(string regNr = "", Boolean onlyParked = true) =>
            (garage is null) ? "" :
                string.Join("\n", garage
                    .Where(item => (item is null) ? false :
                        string.IsNullOrEmpty(regNr) ?
                        item?.IsParked == onlyParked
                        : item?.RegNr.ToLower() == regNr.ToLower() )
                    .ToList()
                    .Select(i => i.ToString()));

        public static string ListGarageCapacity()
        {
            return "Occupancy: " + (garage?.Occupancy ?? 0).ToString()
                + "\n\nCapacity: " + (garage?.Capacity ?? 0).ToString()
                + "\n\nMaxcapacity: " + Garage<Vehicle>.MAX_CAPACITY.ToString();
        }

        public static string ListVehicle(string search)
        {
            var vehicle = garage?.GetVehicle(search) ?? null;
            return ListVehicles(vehicle?.RegNr);
        }

        public static Boolean GarageMissing() => (garage is null);
        public static Boolean GarageIsFull() =>garage.IsFull;

        internal static string StatsVehiclesInGarage()
        {
            if (garage is null) return "";
            var result_list = garage
                 .Where(v => v != null && v.IsParked)
                 .GroupBy(v => v.Type)
                 .Select(group => new { Type = group.Key,Count = group.Count() })
                 .ToList();
            return string.Join("\n", result_list);
        }
 
        public static Vehicle[] GetTestVehicles()
        {
            return Garage<Vehicle>.TestVehicles();
        }


    }
}