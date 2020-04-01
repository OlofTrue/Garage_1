using System;
using System.Collections;

namespace Garage_1
{
    public abstract class Vehicle
    {
        public Vehicle()
        {

        }

        public string RegNr;
        public string Color;
        public virtual int NoWheels { get; set; }
        public Boolean IsParked;
    }
    public class Aiplane: Vehicle
    {
        public override int NoWheels { get; set; } = 2;

}

    public class Motorcycle : Vehicle
    {
        public override int NoWheels { get; set; } = 2;
    }

    public class Car : Vehicle
    {
        public override int NoWheels { get; set; } = 4;
    }

    public class Bus : Vehicle
    {
        public override int NoWheels { get; set; } = 4;
        public  int NoSeats { get; set; }
    }

    public class Boat : Vehicle
    {
        public override int NoWheels { get; set; } = 0;
        public int NoEngines { get; set; } = 1;
        public float Lenght{ get; set; }
    }
}