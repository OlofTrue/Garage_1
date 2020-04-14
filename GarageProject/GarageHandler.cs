using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Garage_1
{
    public static class GarageHandler
    {
        private static Garage<Vehicle> garage;

        public static void SetUpGarage(int cap)
        {
            garage =null;
            if (cap > 0)
            {
                garage = new Garage<Vehicle>(cap);
            }
        }

        public static IGarage<Vehicle> GetGarageCopyForTest(int cap)
        {
            SetUpGarage(cap);
            return (IGarage<Vehicle>)garage;
        }

        public static bool AddVehicle(IVehicle vehicle, out string errMsg)
        {
            errMsg = "";
            var success = false;
            if (garage?.GetVehicle(vehicle?.RegNr) == null)
                success = garage?.AddVehicle((Vehicle)vehicle) ?? false;
            else
                errMsg = string.Format($"Reg.nr {vehicle?.RegNr} already exists");
            return success;
            
        }

        public static Boolean RemoveVehicle(string regNr)
        {
            return garage?.RemoveVehicle(regNr) ?? false;
        }

        public static string ListVehicles(string regNr = "") =>
            (garage is null) ? "" :
                string.Join("\n", garage
                    .Where(item => string.IsNullOrEmpty(regNr) ? true
                        : item?.RegNr.ToLower() == regNr.ToLower())
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

        internal static string ListVehicleG(string search)
        {
            var cntSearchKeys = search.Split(";").Length;
            if (garage is null) return "";
            var result_list = garage
                 .Where(v => (v.MatchAll(search) == cntSearchKeys))
                 .ToList();
            return string.Join("\n", result_list);
        }

        public static Boolean GarageMissing() => (garage is null);
        public static Boolean GarageIsFull() => garage.IsFull;

        internal static string StatsVehiclesInGarage()
        {
            if (garage is null) return "";
            var result_list = garage    //.Where(v => v != null )
                 .GroupBy(v => v.Type)
                 .Select(group => new { Type = group.Key, Count = group.Count() })
                 .ToList();
            return string.Join("\n", result_list) + ((result_list?.Count()>0) ? "\n\n" :  "");
        }

        private static readonly string jsonFile = System.AppDomain.CurrentDomain.BaseDirectory + "garage.json";
        //System.IO.File.Delete(jsonFile);
        internal static void Export() =>JsonSerialization.WriteToJsonFile<System.Collections.Generic.List<Vehicle>>(jsonFile, garage.ToList());

        internal static System.Collections.Generic.List<IVehicle> Import() => JsonSerialization.ReadFromJsonFile<System.Collections.Generic.List<IVehicle>>(jsonFile);
    }
}