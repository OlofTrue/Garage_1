using System.Collections;
using System.Collections.Generic;

namespace Garage_1
{
    public class Garage<T>: IEnumerable<T> where T: Vehicle
    {
        //private readonly int capacity;

        public Garage(int capacity)
        {
            //this.capacity = capacity;
            Capacity = capacity;
        }

        public int Capacity { get; }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}