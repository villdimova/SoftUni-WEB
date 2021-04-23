using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ThreadRace
{
    class Program
    {
        static void Main(string[] args)
        {
            Object num = new object();
            List<int> numbers = Enumerable.Range(0, 1000000).ToList();
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    lock (num)
                    {
                        while (numbers.Count > 0)
                        {

                            numbers.RemoveAt(numbers.Count - 1);
                        }

                    }
                }).Start();

                Console.WriteLine("Success!!!");
            }
        }
    }
}
