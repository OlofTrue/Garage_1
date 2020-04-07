using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Garage_1
{
    public abstract class Vehicle
    {
        public IList GetVehicleProperties()
        {
            return this.GetType().GetProperties()
                 .Select(prop => new
                 {
                     NameP = prop.Name,
                     TypeP = prop.PropertyType.Name,
                 })
                 //.Where(p => p.GetCustomAttribute(typeof(Include)) != null)
                 //.OrderBy(p => p.GetCustomAttribute(typeof(Order)) ?? 99)
                 .ToList();
        }

        public Boolean Match(string strSearch)
        {
            Boolean anytMatch = false;
            foreach (string item in this.ToString().Split(";"))
            {
                foreach (var word in strSearch.Split(";"))
                {
                    if (item.Split("=")[1].ToLower().Trim() == word.ToLower().Trim())
                    {
                        anytMatch = true;
                        break;
                    }
                    if (strSearch.Contains("="))
                    {
                        if (item.ToLower().Replace(" ", "") == word.ToLower().Replace(" ", ""))
                        {
                            anytMatch = true;
                            break;
                        }
                    }
                }
            }
            return anytMatch;
        }
        public virtual Boolean MatchAny(string strSearch) => this.Match(strSearch);

        //[Order(1)]
        //[DisplayName("Reg.nr")]
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
        public override Boolean MatchAny(string strSearch) => this.Match(strSearch) || base.Match(strSearch);
    }

    public class Motorcycle : Vehicle
    {
        public override int NoWheels { get; set; } = 2;
        public int CylinderVolume { get; set; }
        public string FuelType { get; set; }
        public override Boolean MatchAny(string strSearch) => this.Match(strSearch) || base.Match(strSearch);
    }

    public class Car : Vehicle
    {
        public override int NoWheels { get; set; } = 4;
        public string FuelType { get; set; }
        public override Boolean MatchAny(string strSearch) => this.Match(strSearch) || base.Match(strSearch);
    }

    public class Bus : Vehicle
    {
        public override int NoWheels { get; set; } = 4;
        public int NoSeats { get; set; }
        public string FuelType { get; set; }
        public override Boolean MatchAny(string strSearch) => this.Match(strSearch) || base.Match(strSearch);
    }

    public class Boat : Vehicle
    {
        public override int NoWheels { get; set; } = 0;
        public int NoEngines { get; set; } = 1;
        public float Lenght { get; set; }
        public override Boolean MatchAny(string strSearch) => this.Match(strSearch) || base.Match(strSearch);
    }
}