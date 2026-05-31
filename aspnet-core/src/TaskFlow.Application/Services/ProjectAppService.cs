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
        //private readonly TaskFlowApplicationMappers _mapper;
        public ProjectAppService(IRepository<Project, Guid> repository)
            : base(repository)
        {
            //_mapper = mapper;
        }

        //protected override ProjectDto MapToGetOutputDto(Project entity)
        //=> _mapper.ToDto(entity);

        //protected override Project MapToEntity(CreateProjectDto createInput)
        //    => _mapper.ToEntity(createInput);
        //protected override void MapToEntity(UpdateProjectDto updateInput, Project entity)
        //{
        //    entity.Name = updateInput.Name;
        //    entity.Description = updateInput.Description;
        //    entity.Status = updateInput.Status;
        //    entity.StartDate = updateInput.StartDate;
        //    entity.EndDate = updateInput.EndDate;
        //}
    }
}
