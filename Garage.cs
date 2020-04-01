using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestGarage")]
namespace Garage_1
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
        {
        public const int MAX_CAPACITY = 25;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in vehicles)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        internal T[] vehicles; //private
        private int occupancy;

        public Garage(int cap)
        {
            cap = Math.Min(Math.Max(cap, 1), MAX_CAPACITY);
            Capacity = cap;
            this.vehicles = new T[cap];
            this.occupancy = 0;
        }

        public int Capacity { get; }

        public int Occupancy => this.occupancy;

        public Boolean IsFull => (Occupancy >= Capacity);

        public Boolean AddVehicle(T vehicle)
        {
            if (IsFull) return false;
            vehicle.IsParked = true;
            vehicles[occupancy++] = vehicle;
            return true;
        }
        public Boolean RemoveVehicle(string regNr)
        {
            if (occupancy==0) return false;
            int inx = FindVehicle_Inx(regNr);
            Util.RemoveAt(ref vehicles, inx);
            return true;
        }

        public Vehicle FindVehicle(string regNr)
        {
            int inx= FindVehicle_Inx(regNr);
            return (inx < vehicles.Length)? vehicles[inx] :null;
        }

        private int FindVehicle_Inx(string regNr)
        {
            int i;
            for (i = 0; i < vehicles.Length; i++)
            {
                if (regNr == vehicles[i].RegNr) break;
            }
            return (i < vehicles.Length) ? i : -1;
        }

    }
}