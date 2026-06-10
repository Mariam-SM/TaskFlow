using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TaskFlow.Notifications
{
    public class FakeSmsService : ISmsService , ITransientDependency
    {
        private readonly ILogger<FakeSmsService> _logger;

        public FakeSmsService(
            ILogger<FakeSmsService> logger)
        {
            _logger = logger;
        }
        public Task SendAsync(string message)
        {
            _logger.LogInformation("Fake SMS sent: {Message}", message);
            return Task.CompletedTask;
        }
    }
}
