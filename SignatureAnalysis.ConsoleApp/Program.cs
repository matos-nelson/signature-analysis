using Microsoft.Extensions.DependencyInjection;
using SignatureAnalysis.ConsoleApp.Extensions;

namespace SignatureAnalysis.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().ConfigureIoC();

            var argParser = serviceProvider.GetService<ArgParser>();

            var signatureAnalyzer = serviceProvider.GetService<SignatureAnalyzer>();
            signatureAnalyzer.Run(argParser.Parse(args));
        }
    }
}
