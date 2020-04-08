using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("UnitTestGarage")]
namespace Garage_1
{
    public class List<T> : IEnumerable<T> where T : Vehicle
    {
        public const int MAX_CAPACITY = 2500;

        public IEnumerator<T> GetEnumerator()
        {
        if (vehicles != null)
            foreach (var item in vehicles)
            {
                if (item != null)  yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private T[] vehicles; //set internal for testing
        private int occupancy;

        public List(int cap)
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
            // Stack-like vehicle collection
            if (IsFull || vehicle == null) return false;
            vehicles[occupancy++] = vehicle;
            return true;

            //ToDo use Parking lots
            //for (int i = 0; i < Capacity; i++)
            //{
            //    if (vehicles[i] == null) //default
            //    {
            //        vehicles[i] = vehicle;
            //        occupancy++;
            //        return true;
            //    }
            //}
            //return false;
        }
        public Boolean RemoveVehicle(string regNr)
        {
            var inx = GetVehicle_Inx(regNr);
            if (inx >= 0)
            {
                //ToDo use Parking lots 
                //vehicles[inx] = default;
                Util.RemoveAt(ref vehicles, inx); 
                occupancy--;
                return true;
            }
            return false;
        }

        public Vehicle GetVehicle(string regNr)
        {
            int inx = GetVehicle_Inx(regNr);
            return (inx >= 0 && inx < vehicles.Length) ? vehicles[inx] : null;
        }

        private int GetVehicle_Inx(string regNr)
        {
            if (occupancy == 0) return -2; //superfluous
            int i;
            for (i = 0; i < vehicles.Length; i++)
            {
                if (regNr == vehicles[i]?.RegNr) break;
            }
            return (i < vehicles.Length) ? i : -1;
        }
    }
}