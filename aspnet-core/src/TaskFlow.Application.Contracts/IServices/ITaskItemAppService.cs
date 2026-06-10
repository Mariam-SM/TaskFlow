using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DTOs.TaskItems;
using TaskFlow.DTOs.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TaskFlow.IServices
{
    public interface ITaskItemAppService : IApplicationService
    {
        Task<TaskDto> GetAsync(Guid id);
        Task<PagedResultDto<TaskDto>> GetListAsync(TaskListFilterDto input);
        Task<TaskDto> CreateAsync(CreateTaskDto input);
        Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto input);
        Task DeleteAsync(Guid id);
        Task<TaskDto> CompleteAsync(Guid id);
        Task<TaskSummaryDto> SummarizeOverdueTasksAsync();
    }
}
