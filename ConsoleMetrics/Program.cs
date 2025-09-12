using StatsdClient;

namespace ConsoleMetrics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new StatsdConfig
            {
                StatsdServerName = "192.168.1.224", // ou o IP do container, se não estiver em host network
                StatsdPort = 8125
            };

            Console.WriteLine("Hello, World!\n");

            using (var dogStatsdService = new DogStatsdService())
            {
                if (!dogStatsdService.Configure(config))
                    throw new InvalidOperationException("Impossivel inicializar DogStatsdService :-(");

                var incremento = 1;
                var decremento = 1;
                var quantidade = 2;
                var histograma = 5;

                var cpu = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
                var cpuUsage = cpu.NextValue();


                while (true)
                {
                    dogStatsdService.Increment("net.leobreda.increment", incremento, tags: new[] { "environment:dev" });
                    dogStatsdService.Decrement("net.leobreda.decrement", decremento, tags: new[] { "environment:dev" });
                    dogStatsdService.Counter("net.leobreda.counter", quantidade, tags: new[] { "environment:dev" });
                    dogStatsdService.Histogram("net.leobreda.histogram", histograma, tags: new[] { "environment:dev" });

                    cpuUsage = cpu.NextValue();
                    dogStatsdService.Gauge("net.leobreda.cpu", cpuUsage, tags: new[] { "environment:dev" });

                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}