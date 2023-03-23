using System.Threading.Tasks;

namespace marvin
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}