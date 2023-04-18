/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        public static ThreadWithState thread;
        static void Main1(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            thread = new ThreadWithState(10,
                new ThreadCallback(ResultCallback)
            );

            Thread thread1 = new Thread(new ThreadStart(thread.ThreadProc));
            thread1.Start();
            Console.WriteLine("Main thread waiting for recursive call.");
            thread1.Join();
            Console.WriteLine("Recursive task completed");
            Console.WriteLine("Main thread End.");

            Console.ReadLine();
        }

        public static void ResultCallback(int lineCount)
        {
            Console.WriteLine("Recursive task {0}.", lineCount);
            Thread thread1 = new Thread(new ThreadStart(thread.ThreadProc));
            thread1.Start();
            thread1.Join();
        }
    }

    public delegate void ThreadCallback(int lineCount);

    public class ThreadWithState
    {
        private int value;

        private ThreadCallback callback;

        public ThreadWithState(int number,
            ThreadCallback callbackDelegate)
        {
            value = number;
            callback = callbackDelegate;
        }

        public void ThreadProc()
        {
            if (callback != null && value > 0)
                callback(value--);
        }
    }

}
