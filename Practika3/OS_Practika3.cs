using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OS_Practika3
{
    class Factory
    {
        private ChannelWriter<int> Writer;
        public Factory(ChannelWriter<int> _writer, CancellationToken tunnel)
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
                    Console.WriteLine("\nПроизводитель остановлен.");
                    Program.endF = Program.endF + 1;
                    return;
                }
                if (Program.tumbler && Program.count <= 100 && Program.count > -1)
                {
                    var product = r.Next(1, 101);
                    await Writer.WriteAsync(product);
                    Program.count += 1;
                    Console.WriteLine($"Отправленные продукты: {product}" + $" Количество эл-тов:  {Program.count}");
                }
            }
        }
    }
    class Consumer
    {
        static public int Count = 0;
        private ChannelReader<int> Reader;
        public Consumer(ChannelReader<int> _reader, CancellationToken tunnel)
        {
            Reader = _reader;
            Task.WaitAll(Run(tunnel));
        }
        private async Task Run(CancellationToken tunnel)
        {
            while (await Reader.WaitToReadAsync())
            {
                if (Consumer.Count >= 0)
                {
                    var product = await Reader.ReadAsync();
                    Program.count -= 1;
                    if (Program.count == -1)
                    {
                        Program.count = 0;
                    }
                    Console.WriteLine($"Выставленные продукты: {product}" + $" Количество эл-тов:  {Program.count}");
                }
                if (Consumer.Count >= 100)
                {
                    Program.tumbler = false;
                }
                else if (Consumer.Count <= 80)
                {
                    Program.tumbler = true;
                }
                if (tunnel.IsCancellationRequested)
                {
                    if ((Consumer.Count == 0) && (Program.endF == 3))
                    {
                        Console.WriteLine("\nВсе продукты потреблены.");
                        return;
                    }
                }
            }
        }
    }
    class Program
    {
        static public int endF = 0;
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
                                channels[i] = Task.Run(() => { new Factory(channel.Writer, sends.Token); }, sends.Token);
                            }
                            else
                            {
                                channels[i] = Task.Run(() => { new Consumer(channel.Reader, sends.Token); }, sends.Token);
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
            Program.endF = 0;
            MainMenu();
        }
    }
}