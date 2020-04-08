using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Garage_1
{
    public abstract class Vehicle
    {
        public IList GetVehicleProperties()
        {
            return this.GetType().GetProperties()
                 .OrderBy(p => (p.GetCustomAttributes(true).Length>0) ? 1 : 99) //ToDo hack works whit any attribute! fix Order
                 .Select(prop => new
                 {
                     NameP = prop.Name,
                     TypeP = prop.PropertyType.Name,
                 }).ToList();
        }

        public int MatchAll(string strSearch)
        {
            int cntMatch = 0;
            Boolean fullProp = (strSearch.Contains("="));
            foreach (string item in this.ToString().Split(";"))
            {
                foreach (var word in strSearch.Split(";"))
                {
                    if (fullProp)
                    {
                        if (item.ToLower().Replace(" ", "") == word.ToLower().Replace(" ", ""))
                            cntMatch++;
                    }
                    else
                    {
                        if (item.Split("=")[1].ToLower().Trim() == word.ToLower().Trim())
                            cntMatch++;
                    }
                }
            }
            return cntMatch;
        }

        //[Order(1)] ToDo namespace!
        [DisplayName("Reg.nr")]
        //[Unique=1]  
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
        public override int NoWheels { get; set; } = 0;
        public int NoEngines { get; set; } = 1;
        public float Lenght { get; set; }
    }
}