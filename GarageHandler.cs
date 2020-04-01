using System;
using System.Collections.Generic;
using System.Text;

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

        public static void ListParkedVehicles(Garage<Vehicle> garage)
        {
            throw new NotImplementedException();
        }

        public static void ListParkedVehiclesByType(Garage<Vehicle> garage)
        {
            throw new NotImplementedException();
        }

        public static Vehicle FindVehicle(Garage<Vehicle> garage,string search)
        {
            return garage.FindVehicle(search);
            //throw new NotImplementedException();
        }

        public static void Park(Vehicle vehicle) => vehicle.IsParked = true;
        public static void UnPark(Vehicle vehicle) => vehicle.IsParked = false;
    }
}
