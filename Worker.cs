using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloAKS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private int _primeNumberPosition = 0;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Prime number at position {position} at {time} is {value}", _primeNumberPosition, DateTimeOffset.Now, FindPrimeNumber(_primeNumberPosition));
                _primeNumberPosition = (long)_primeNumberPosition + 100 > int.MaxValue ? 1 : _primeNumberPosition += 100;
            }

            _logger.LogInformation("Exiting worker at {time}", DateTimeOffset.Now);
        }

        public static long FindPrimeNumber(int n)
        {
            var count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                var prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }

                    b++;
                }

                if (prime > 0)
                {
                    count++;
                }

                a++;
            }

            return --a;
        }
    }
}