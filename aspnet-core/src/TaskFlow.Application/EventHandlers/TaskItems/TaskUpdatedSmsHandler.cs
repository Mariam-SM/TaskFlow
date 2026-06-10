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
    public class TaskUpdatedSmsHandler :
        ILocalEventHandler<EntityUpdatedEventData<TaskItem>>,
         ITransientDependency
    {
        private readonly ISmsService _smsService;
        public TaskUpdatedSmsHandler(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task HandleEventAsync(EntityUpdatedEventData<TaskItem> eventData)
        {
            var taskItem = eventData.Entity;

            await _smsService.SendAsync($"Task Updated: {taskItem.Title} - Status: {taskItem.TaskStatus}");
        }
    }
}
