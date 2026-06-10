using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Notifications
{
    public interface ISmsService
    {
        Task SendAsync(string message);
    }
}
