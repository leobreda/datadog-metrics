using StatsdClient;

namespace ConsoleMetrics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new StatsdConfig
            {
                StatsdServerName = "127.0.0.1",
                StatsdPort = 8125
            };

            //Apontando pro container, ao rodar no Visual Studio
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                config.StatsdServerName = "192.168.0.224";
            

            Console.WriteLine("Hello, World!\n");

            using (var dogStatsdService = new DogStatsdService())
            { 
                var quantidade = 1;

                var histograma = 5;

                var cpu = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
                
                var cpuUsage = cpu.NextValue();

                string[] tags = new[] { "environment:dev" };


                int i = 0;
                while (true)
                {
                    i++;

                    dogStatsdService.Counter("net.leobreda.count", quantidade, 1, tags);

                    dogStatsdService.Increment("net.leobreda.increment", i, 1,tags);

                    dogStatsdService.Decrement("net.leobreda.decrement", i, 1, tags);

                    dogStatsdService.Distribution("net.leobreda.distribuition", i, 1, tags);

                    dogStatsdService.Histogram("net.leobreda.histogram", histograma,1, tags);

                    cpuUsage = cpu.NextValue();
                    dogStatsdService.Gauge("net.leobreda.cpu", cpuUsage,1, tags);

                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);

                    if (i % 10 == 0)
                        Console.WriteLine($"{i}s ...");
                }
            }
        }
    }
}