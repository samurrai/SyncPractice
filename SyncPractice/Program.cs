using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncPractice
{
    class Program
    {
        private static User user = new User();
        private static object lockObject = new object();

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[20];

            for (int i = 0; i < threads.Length / 2; i++)
            {
                Thread thread = new Thread(ArriveMoney);
                thread.Name = $"Поток номер {thread.ManagedThreadId}";
                threads[i] = thread;
            }

            for (int i = threads.Length / 2; i < threads.Length; i++)
            {
                Thread thread = new Thread(WithdrawMoney);
                thread.Name = $"Поток номер {thread.ManagedThreadId}";
                threads[i] = thread;
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.ReadLine();

            Console.WriteLine($"Конечный счет пользователя - {user.Money}");
            Console.ReadLine();
        }

        //********Методы с синхронизацией********

        private static void ArriveMoney()
        {
            lock (lockObject)
            {

                var currentThread = Thread.CurrentThread;

                Console.WriteLine($"{currentThread.Name} начал свою работу");

                Thread.Sleep(new Random().Next(2000));

                user.Money += 1000;
                Console.WriteLine(user.Money);

                Console.WriteLine($"{currentThread.Name} завершил свою работу\n");
            }
        }

        private static void WithdrawMoney()
        {
            lock (lockObject)
            {
                var currentThread = Thread.CurrentThread;

                Console.WriteLine($"{currentThread.Name} начал свою работу");

                Thread.Sleep(new Random().Next(2000));

                user.Money -= 1000;
                Console.WriteLine(user.Money);

                Console.WriteLine($"{currentThread.Name} завершил свою работу\n");
            }
        }

        //********Методы без синхронизации********

        //private static void ArriveMoney()
        //{
        //    var currentThread = Thread.CurrentThread;

        //    Console.WriteLine($"{currentThread.Name} начал свою работу");

        //    Thread.Sleep(new Random().Next(2000));

        //    user.Money += 1000;
        //    Console.WriteLine(user.Money);

        //    Console.WriteLine($"{currentThread.Name} завершил свою работу\n");
        //}

        //private static void WithdrawMoney()
        //{
        //    var currentThread = Thread.CurrentThread;

        //    Console.WriteLine($"{currentThread.Name} начал свою работу");

        //    Thread.Sleep(new Random().Next(2000));

        //    user.Money -= 1000;
        //    Console.WriteLine(user.Money);

        //    Console.WriteLine($"{currentThread.Name} завершил свою работу\n");
        //}
    }
}
