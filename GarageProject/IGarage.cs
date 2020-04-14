using System.Collections.Generic;

namespace Garage_1
{
    public interface IGarage<T> :IEnumerable<T>
    {
        int Capacity { get; }
        bool IsFull { get; }
        int Occupancy { get; }

        bool AddVehicle(T vehicle);
        IEnumerator<T> GetEnumerator();
        IVehicle GetVehicle(string regNr);
        bool RemoveVehicle(string regNr);
    }
}