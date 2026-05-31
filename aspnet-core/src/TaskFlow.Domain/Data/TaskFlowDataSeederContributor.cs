using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Projects;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Timing;

namespace TaskFlow.Data
{
    public class TaskFlowDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<TaskItem, Guid> _taskRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClock _clock;

        public TaskFlowDataSeederContributor(
            IRepository<Project, Guid> projectRepository,
            IRepository<TaskItem, Guid> taskRepository,
            IIdentityUserRepository userRepository,
            IGuidGenerator guidGenerator,
            IClock clock)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _guidGenerator = guidGenerator;
            _clock = clock;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedProjectsAsync();
        }

        private async Task SeedProjectsAsync()
        {
            if (await _projectRepository.AnyAsync())
                return;

            var adminUser = await _userRepository.FindByNormalizedUserNameAsync("ADMIN");
            if (adminUser == null)
                return;

            var project = new Project(
                _guidGenerator.Create(),
                "TaskFlow Demo Project",
                "Sample project for testing",
                ProjectStatus.Active,
                _clock.Now,
                _clock.Now.AddMonths(3)
            );

            await _projectRepository.InsertAsync(project);

            var task = new TaskItem(
                _guidGenerator.Create(),
                project.Id,
                "First Demo Task",
                "This is a seeded task for testing",
                TaskPriority.Medium,
                _clock.Now.AddDays(7)
            );

            task.AssignedToUserId = adminUser.Id;
            task.TaskStatus = TaskItemStatus.Todo;

            await _taskRepository.InsertAsync(task);
        }
    }
}
