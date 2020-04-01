using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_1
{

    public class Util
    {
        #pragma warning disable IDE0059, IDE0060
        public static int InputPositivInt(string askInput)
        {
            var num = -1;
            do
            {
                Console.Clear();
                Console.Write($"{askInput} ");
                num = int.TryParse(Console.ReadLine(), out num) ? num : -1;
            } while (num<0);
            return num;
        }

        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            int a;
            for (a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            //T t = (T)Activator.CreateInstance(typeof(T));
            //arr[a - 1] = t;
            Array.Clear(arr,a - 1,1);
        }


    }
}
