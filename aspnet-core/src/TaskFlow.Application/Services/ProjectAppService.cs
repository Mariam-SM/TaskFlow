using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.DTOs.Projects;
using TaskFlow.Entities.Projects;
using TaskFlow.IServices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TaskFlow.Services
{
    public class ProjectAppService : CrudAppService<
                               Project,
                               ProjectDto,
                               Guid,
                               PagedAndSortedResultRequestDto,
                               CreateProjectDto,
                               UpdateProjectDto>
        ,
       IProjectAppService
    {
        public ProjectAppService(IRepository<Project, Guid> repository)
            : base(repository)
        {
        }

    }
}
