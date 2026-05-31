using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.DTOs.Tasks;
using TaskFlow.Entities.TaskItems;

namespace TaskFlow.Mapping
{
    public class TaskItemMapping : Profile
    {
        public TaskItemMapping()
        {
            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<TaskItem, TaskDto>();
        }
    }
}
