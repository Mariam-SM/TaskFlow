using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.DTOs.Projects;
using TaskFlow.Entities.Projects;
using Volo.Abp.Account;

namespace TaskFlow.Mapping
{
    public class ProjectMapping : Profile 
    {
        public ProjectMapping()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
        }
    }
}
