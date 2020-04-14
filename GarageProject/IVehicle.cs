using System.Collections;

namespace Garage_1
{
    public interface IVehicle
    {
        string Color { get; set; }
        int NoWheels { get; set; }
        string RegNr { get; set; }
        string Type { get; }

        IList GetVehicleProperties();
        int MatchAll(string strSearch);
        string ToString();
    }
}