/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {

        public static ConcurrentBag<int> bag = new ConcurrentBag<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            Task[] collection = new Task[1];
            collection[0] = Task.Factory.StartNew(AddElementToCollection);

            // Wait for all tasks to complete
            Task.WaitAll(collection);

            Console.ReadLine();
        }

        static void AddElementToCollection()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                bag.Add(rnd.Next(1, 50));
                Task task = Task.Factory.StartNew(PrintCollection);
                Task.WaitAll(task);
            }
        }

        static void PrintCollection()
        {
            Console.Write("[");
            foreach (var item in bag)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine("\b\b]");
        }
    }
}
