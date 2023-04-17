/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            await Task.Run(() =>
            {
                int Min = 0;
                int Max = 20;

                int[] array = new int[10];

                Random rnd = new Random();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rnd.Next(Min, Max);
                }
                Output(array);
                return array;
            }).ContinueWith(antecedent =>
            {
                int[] test2 = antecedent.Result;
                int randomNumber = new Random().Next(1, 27);
                Console.WriteLine("Random Number = " + randomNumber);
                for (int i = 0; i < test2.Length; i++)
                {
                    test2[i] *= randomNumber;
                }
                Output(test2);
                return test2;
            }).ContinueWith(antecedent => {
                int[] test2 = antecedent.Result;
                Array.Sort(test2);
                Output(test2);
                return test2;
            }).ContinueWith(antecedent => {
                int[] test2 = antecedent.Result;
                Console.WriteLine(test2.Average());
            });

            Console.ReadLine();
        }
        static void Output(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
