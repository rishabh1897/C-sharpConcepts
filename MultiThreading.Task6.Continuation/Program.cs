/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void task4()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            //var task1 = Task.Factory.StartNew(() =>
            //{
            var task2 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        Console.WriteLine("Loop cancelled.");
                        cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        break;
                    }
                }
            }, cancellationTokenSource.Token);
            var task3 = task2.ContinueWith((value) =>
            {
                Console.WriteLine("Parent Loop cancelled.");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Default);
            //});
            Console.WriteLine("Enter to cancel");
            Console.ReadKey();
            cancellationTokenSource.Cancel();
            Console.WriteLine("Waiting To Be Cancelled");

            //task2.Wait();

            Console.WriteLine("Task Cancelled");

            Console.ReadKey();
        }
        static void continuetask1()
        {
            Console.WriteLine("Main Method Started");
            //Creating the Parent Task
            var parentTask = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent Task Started");
                //Creating the Child Task
                var childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("Child Task Started.");
                    //Thread.Sleep(5000);
                    Console.WriteLine("Child Task Completed");
                    //return "Task Completed";
                });
                Console.WriteLine("Parent Task Completed");
                // Parent Task will wait for detached Child Task to complete its execution
                //return childTask.Result;
            });
            //Waiting for the Parent Task to be completed. Not the Child Task
            parentTask.Wait();
            //Here, parentTask.Result will block the Main thread, till the Parent task complete its execution
            //Console.WriteLine($"Parent Task Returned: {parentTask.Result}");
            Console.WriteLine("Main Method Completed");
            Console.ReadKey();
        }
        static void continuetask2()
        {
            Task parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Starting child task...");

                Task child = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child running. Going to sleep for a sec.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Child finished and throws an exception.");
                    Console.WriteLine("Parent task thread id:");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);
                child.ContinueWith(antecedent =>
                {
                    // write out a message and wait
                    Console.WriteLine("Continuation of child task running");
                    Thread.Sleep(1000);
                    Console.WriteLine("Continuation finished");
                    Console.WriteLine("Child task thread id:", Task.CurrentId);
                }, CancellationToken.None, TaskContinuationOptions.AttachedToParent
                    | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);
            });

            try
            {
                Console.WriteLine("Waiting for parent task");
                parent.Wait();
                Console.WriteLine("Parent task finished");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception: {0}", ex.InnerException.GetType());
            }
        }
        //static int DivideBy(int divisor)
        //{
        //    Thread.Sleep(2000);
        //    return 10 / divisor;
        //}
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            //// feel free to add your code
            ///
            //continuetask2();
            //Console.ReadLine();
            task4();
            //Task<int> task = Task.Run(() =>
            //{
            //    return 10;

            //});
            //task.ContinueWith((i) =>
            //{
            //    Console.WriteLine("TasK Canceled");
            //}, TaskContinuationOptions.OnlyOnCanceled);
            //task.ContinueWith((i) =>
            //{
            //    Console.WriteLine("Task Faulted");
            //}, TaskContinuationOptions.OnlyOnFaulted);
            //var completedTask = task.ContinueWith((i) =>
            //{
            //    Console.WriteLine("Task Completed");
            //}, TaskContinuationOptions.OnlyOnRanToCompletion);
            //completedTask.Wait();
            //Console.ReadLine();




        }

    }

}
