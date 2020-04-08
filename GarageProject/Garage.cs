﻿using System;
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

        private T[] vehicles; //internal
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

            // Parking lots
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
                Util.RemoveAt(ref vehicles, inx); // vehicles[inx] = default;
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


        public static Vehicle[] TestVehicles() => new Vehicle[]
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