using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OS_practika3
{
    class Creater
    {
        private ChannelWriter<int> Writer;
        public Creater(ChannelWriter<int> _writer, CancellationToken tunnel)
        {
            Writer = _writer;
            Task.WaitAll(Run(tunnel));
        }

        private async Task Run(CancellationToken tunnel)
        {
            var r = new Random();
            while (await Writer.WaitToWriteAsync())
            {
                if (tunnel.IsCancellationRequested)
                {
                    Console.WriteLine("Программа остановлена.");
                    return;
                }
                if (Program.tumbler && Program.count <= 100 && Program.count > -1)
                {
                    var product = r.Next(1, 101);
                    await Writer.WriteAsync(product);
                    Program.count += 1;
                    Console.WriteLine($"Отправленные продукты: {product}" + $" размер  {Program.count}");
                }
            }
        }
    }
    class Reader
    {
        static public int Count = 0;
    }
    class Buyer
    {
        static public int Count = 0;
        private ChannelReader<int> Reader;

        public Buyer(ChannelReader<int> _reader, CancellationToken tunnel)
        {
            Reader = _reader;

            Task.WaitAll(Run(tunnel));
        }

        private async Task Run(CancellationToken tunnel)
        {
            while (await Reader.WaitToReadAsync())
            {

                if (Buyer.Count >= 0)
                {
                    var product = await Reader.ReadAsync();
                    Program.count -= 1;
                    if (Program.count == -1)
                    {
                        Program.count = 0;
                    }

                    Console.WriteLine($"Выставленные продукты: {product}" + $" размер  {Program.count}");


                }
                if (Buyer.Count >= 100)
                {
                    Program.tumbler = false;
                }
                else if (Buyer.Count <= 80)
                {
                    Program.tumbler = true;
                }

                if (tunnel.IsCancellationRequested)
                {
                    if (Buyer.Count == 0)
                    {
                        Console.WriteLine("Потребление остановлено.");
                        return;
                    }
                }
            }
        }
    }

    class Program
    {
        static public bool tumbler = true;
        static public int count = 0;

        static void MainMenu()
        {

            bool tumbler = true;
            while (tumbler)
            {
                Console.WriteLine("Главное меню");
                Console.WriteLine("\n1. Запустить задание.");
                Console.WriteLine("\n2. Очистить окно программы.");
                Console.WriteLine("\n0. Выйти из программы.");
                Console.Write("\nВыберите пункт меню >> ");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:

                        Channel<int> channel = Channel.CreateBounded<int>(200);
                        var sends = new CancellationTokenSource();

                        Task[] channels = new Task[5];
                        for (int i = 0; i < 5; i++)
                        {
                            if (i < 3)
                            {
                                channels[i] = Task.Run(() => { new Creater(channel.Writer, sends.Token); }, sends.Token);
                            }
                            else
                            {
                                channels[i] = Task.Run(() => { new Buyer(channel.Reader, sends.Token); }, sends.Token);
                            }
                        }
                        new Thread(() =>
                        {
                            bool tumbler2 = true;
                            while (tumbler2 is true)
                            {
                                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                                {
                                    sends.Cancel();
                                    tumbler2 = false;
                                }
                            }
                        })
                        { IsBackground = true }.Start();
                        Task.WaitAll(channels);
                        break;

                    case 0:
                        tumbler = false;
                        break;

                    case 9:
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Пункт меню не существует");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}