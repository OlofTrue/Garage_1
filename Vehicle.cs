using System;
using System.Collections;
using System.Text;

namespace Garage_1
{
    public abstract class Vehicle
    {
        public string RegNr { get; set; }
        public string Color { get; set; }
        public virtual int NoWheels { get; set; }
        public Boolean IsParked { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var prop in this.GetType().GetProperties())
            {
                result.Append(prop.Name + "=");
                result.Append(prop.GetValue(this, null) + ";");
            }
            return (result.ToString() + ";").Replace(";;", "");
        }
    }
    public class Aiplane : Vehicle
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
        public int NoSeats { get; set; }
    }

    public class Boat : Vehicle
    {
        public override int NoWheels { get; set; } = 0;
        public int NoEngines { get; set; } = 1;
        public float Lenght { get; set; }
    }
}