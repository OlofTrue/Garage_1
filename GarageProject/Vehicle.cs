using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Garage_1
{
    public abstract class Vehicle
    {
        //public Vehicle(string regNr, string color)
        //{
        //    RegNr = regNr;
        //    Color = color
        //}
        public IList GetVehicleProperties()
        {
            return this.GetType().GetProperties()
                 .Select(prop => new {
                     NameP = prop.Name,
                     TypeP = prop.PropertyType.Name
                 })
                 .ToList();
        }

        public virtual Boolean Match(string strSearch)
        {
            if (strSearch == "")  return true;
            return false;
        }

        //[Display(Order = 1)]
        public string RegNr { get; set; }
        public string Color { get; set; }
        public virtual int NoWheels { get; set; }
        //public Boolean IsParked { get; }
        public string Type => this.GetType().Name; 
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
    public class Airplane : Vehicle
    {
        public override int NoWheels { get; set; } = 2;
        public string FuelType { get; set; }

    }

    public class Motorcycle : Vehicle
    {
        public override int NoWheels { get; set; } = 2;
        public int CylinderVolume { get; set; }
        public string FuelType { get; set; }
    }

    public class Car : Vehicle
    {
        //public Car(string regNr, string color) : base(regNr, color)
        //{

        //}
        public override int NoWheels { get; set; } = 4;
        public string FuelType { get; set; }
    }

    public class Bus : Vehicle
    {
        public override int NoWheels { get; set; } = 4;
        public int NoSeats { get; set; }
        public string FuelType { get; set; }
    }

    public class Boat : Vehicle
    {
        //public Boat(string regNr, string color,int NoEngines, float Lenght) : base( regNr,  color)
        //{

        //}
        public override int NoWheels { get; set; } = 0;
        public int NoEngines { get; set; } = 1;
        public float Lenght { get; set; }
    }

}