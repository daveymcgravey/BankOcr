using BankOCR.Services;
using BankOCR.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankOCR
{
    // https://codingdojo.org/kata/BankOCR/

    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((_, services) =>
                   services
                    .AddTransient<IAccountNumberParsingService, AccountNumberParsingService>()
                    .AddTransient<IAccountNumberValidationService, AccountNumberValidationService>()
                    .AddTransient<IOcrInputReaderService, OcrInputReaderService>()
               );
    }
}
