using System;
using System.Threading;
using System.Collections.Generic;

namespace OS_Practika6
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            int mem_fullness = 0;
            int mem_size = 65536;
            int task_mem_min = 0;
            int task_mem_max = 65535;
            bool wait_flag = false;
            Start();
            Console.WriteLine("Программа закончила свою работу.");
            void Start()
            {
                Console.Clear();
                Console.WriteLine("Практическая работа №6. Вариант №3.\n\n");
                Console.WriteLine("1. Начать выполнение.\n");
                Console.WriteLine("2. Добавить еще задачу.\n");
                Console.WriteLine("3. Посмотреть ресурсы памяти.\n");
                while (true)
                {
                    if (Console.ReadLine() == "1") break;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Возможен выбор только первого варианта.");
                        Console.WriteLine("1. Начать выполнение.\n");
                        Console.WriteLine("2. Добавить еще задачу.\n");
                        Console.WriteLine("3. Посмотреть ресурсы памяти.\n");
                    }
                }
                Thread wait_k1 = new Thread(wait_add_key);
                wait_k1.Start();
                Thread wait_k2 = new Thread(wait_info_key);
                wait_k2.Start();
                add_task();
                while (true) Thread.Sleep(100);
            }
            void add_task()
            {
                Random rnd = new Random();
                int task_mem_current = rnd.Next(task_mem_min, task_mem_max);

                if (mem_size - mem_fullness >= task_mem_current)
                {
                    if (queue.Count == 0)
                    {
                        Thread thread = new Thread(() => load_task_to_mem(task_mem_current));
                        thread.Start();
                    }
                    else
                    {
                        int first_task = queue.Dequeue();
                        Thread thread = new Thread(() => load_task_to_mem(first_task));
                        while (true)
                        {
                            if (mem_size - mem_fullness >= first_task)
                            {
                                thread.Start();
                                break;
                            }
                            else queue.Enqueue(task_mem_current);
                        }
                    }
                }
                else
                {
                    // Добавить таск в очередь
                    queue.Enqueue(task_mem_current);
                    Console.WriteLine("Невозможно выделить нужное количество памяти. Задача ставится в очередь. Размер задачи: {0} байт.", task_mem_current);
                }
            }
            void load_task_to_mem(int task_mem_current)
            {
                Console.WriteLine("\nЗадача запущена...");
                Console.WriteLine("Память заполнена на {0} байт.", task_mem_current);
                mem_fullness += task_mem_current;
                Thread.Sleep(5000);
                mem_fullness -= task_mem_current;
                Console.WriteLine("Память освобождена на {0} байт.", task_mem_current);
            }
            void wait_add_key()
            {
                while (true)
                {
                    Thread.Sleep(100);
                    if (Console.ReadKey(true).Key == ConsoleKey.D2)
                    {
                        wait_flag = true;
                        add_task();
                    }
                }
            }
            void wait_info_key()
            {
                while (true)
                {
                    Thread.Sleep(100);
                    if (Console.ReadKey(true).Key == ConsoleKey.D3)
                    {
                        string q_str = "";
                        int[] arr = queue.ToArray();
                        foreach (int ob in arr)
                        {
                            q_str = q_str + ob.ToString() + ";  ";
                        }
                        Console.WriteLine("Заполненность памяти - {0} байт.", mem_fullness);
                    }
                }
            }
        }
    }
}