using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class ClassSemaphore
    {
        static int value = 10;
        private static Semaphore _sem;

        static void Main(string[] args)
        {
            Console.WriteLine("Main thread starts.");
            _sem = new Semaphore(initialCount: 1, maximumCount: 2);
            for (int i = 1; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(DecrementValueAndPrint, i);
            }
            Console.ReadLine();
            Console.WriteLine("Main thread exits.");
        }

        public static void DecrementValueAndPrint(Object state)
        {
            Console.WriteLine("Thread {0} begins and waits for the semaphore.", state);
            Console.WriteLine();

            _sem.WaitOne();

            Console.WriteLine("Thread {0} enters the semaphore and value = {1}", state, value--);

            Thread.Sleep(500);

            Console.WriteLine("Thread {0} releases the semaphore.", state);

            Console.WriteLine();

            _sem.Release();
        }
    }
}
