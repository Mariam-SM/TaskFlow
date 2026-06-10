using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace TaskFlow.EventHandlers.TaskItems
{
    public class TaskCreatedSmsHandler :
        ILocalEventHandler<EntityCreatedEventData<TaskItem>>,
        ITransientDependency
    {
        private readonly ISmsService _smsService;

        public TaskCreatedSmsHandler(
            ISmsService smsService)
        {
            _smsService = smsService;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<TaskItem> eventData)
        {
            var task = eventData.Entity;

            await _smsService.SendAsync(
                $"Task Created: {task.Title}");

        }
    }
}
