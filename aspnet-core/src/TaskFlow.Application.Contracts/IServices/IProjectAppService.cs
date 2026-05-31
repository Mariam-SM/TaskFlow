using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.DTOs.Projects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TaskFlow.IServices
{
    public interface IProjectAppService : 
        ICrudAppService<
        ProjectDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateProjectDto,
        UpdateProjectDto>
    {

    }
}
