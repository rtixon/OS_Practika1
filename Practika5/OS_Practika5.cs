using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace OS_Practika5
{
    class OS_Practika5
    {
        static void Main(string[] args)
        {
            int count = 3;
            bool[] glob_flag = {false, false, false};
            bool[] glob_isAlive = {true, true, true};
            bool wait_key_flag = true;
            int threads_counter = 0;
            bool flag = true;
            string m = ".";
            Console.WriteLine("Введите номер меню:\n");
            Console.WriteLine("1. Начать работу планировщика и модулей.");
            Console.WriteLine("0. Выход из программы.");
            Console.WriteLine("\n>>");
            m = Console.ReadLine();
            switch (m)
            {
                case "1":
                    Console.Clear();
                    Start();
                    break;
                case "0":
                    break;
            }
            Console.WriteLine("Программа закончила свою работу.");
            void Start()
            {
                Console.WriteLine("\nЗапущен планировщик задач.\n");
                Queue<Thread> queue = new Queue<Thread>();
                //  Init of threads queue ----
                Console.WriteLine("Добавление первого потока.");
                Thread mod_1 = new Thread(module_1);
                queue.Enqueue(mod_1);
                Thread.Sleep(1000);
                Console.WriteLine("Добавление второго потока.");
                Thread mod_2 = new Thread(module_2);
                queue.Enqueue(mod_2);
                Thread.Sleep(1000);
                Console.WriteLine("Добавление третьего потока.");
                Thread mod_3 = new Thread(module_3);
                queue.Enqueue(mod_3);
                Thread.Sleep(1000);
                while (true)
                {
                    glob_flag[threads_counter] = false;
                    if (count == 0) break;
                    if (glob_isAlive[threads_counter] == true)
                    {
                        Thread first_thread = queue.Dequeue();
                        DateTime Start = DateTime.Now;
                        try
                        {
                            first_thread.Start();
                        }
                        catch
                        {
                            glob_flag[threads_counter] = false;
                        }
                        wait_key_flag = true;
                        Thread wait_k = new Thread(wait_key);
                        wait_k.Start();
                        flag = true;
                        while (true)
                        {
                            if (flag == false)
                            {
                                queue.Enqueue(first_thread);
                                break;
                            }
                            if (first_thread.IsAlive == false)
                            {
                                flag = false;
                                glob_flag[threads_counter] = false;
                                wait_key_flag = false;
                                break;
                            }
                            if ((float)DateTime.Now.Subtract(Start).TotalSeconds >= 6 && (glob_isAlive[threads_counter] != false))
                            {
                                flag = false;
                                glob_flag[threads_counter] = true;
                                queue.Enqueue(first_thread);
                                wait_key_flag = false;
                                Console.WriteLine("Поток остановлен - слишком долгое время выполнения.");
                                break;
                            }
                        }
                    }
                    if (threads_counter == 2) threads_counter = 0;
                    else threads_counter++;
                }
                Console.WriteLine("\nВсе потоки закончили свою работу.\n");
                Thread.Sleep(5000);
            }
            void wait_key()
            {
                while (true)
                {
                    Thread.Sleep(100);
                    if (wait_key_flag != true) break;
                    if (Console.ReadKey(true).Key == ConsoleKey.Q && wait_key_flag)
                    {
                        Console.WriteLine("Поток остановлен.\n");
                        flag = false;
                        glob_flag[threads_counter] = true;
                        break;
                    }
                }
            }
            void module_1()
            {
                Console.WriteLine("\nПоток 1 запущен");
                for (int item = 0; item < 5; item++)
                {
                    while (glob_flag[0] == true) Thread.Sleep(100);
                    Console.WriteLine("Поток 1 работает.");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Поток 1 закончил свою работу.");
                glob_isAlive[0] = false;
                count--;
                Thread.Sleep(1000);
            }
            void module_2()
            {
                Console.WriteLine("\nПоток 2 запущен.");
                for (int item = 0; item < 14; item++)
                {
                    while (glob_flag[1] == true) Thread.Sleep(100);
                    Console.WriteLine("Поток 2 работает.");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Поток 2 закончил свою работу.");
                glob_isAlive[1] = false;
                count--;
                Thread.Sleep(1000);
            }
            void module_3()
            {
                Console.WriteLine("\nПоток 3 запущен.");
                for (int item = 0; item < 9; item++)
                {
                    while (glob_flag[2] == true) Thread.Sleep(100);
                    Console.WriteLine("Поток 3 работает.");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Поток 3 закончил свою работу.");
                glob_isAlive[2] = false;
                count--;
                Thread.Sleep(1000);
            }
        }
    }
}
