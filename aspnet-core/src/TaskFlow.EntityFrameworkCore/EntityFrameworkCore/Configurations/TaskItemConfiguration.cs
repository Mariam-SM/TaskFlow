using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Entities.TaskItems;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TaskFlow.EntityFrameworkCore.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable("TaskItems");

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(TaskFlowConsts.MaxTitleLength);

            builder.Property(t => t.Description)
                   .HasMaxLength(TaskFlowConsts.MaxDefaultDescriptionLength);

            builder.HasMany(t => t.Comments)
                   .WithOne(c => c.Task)
                   .HasForeignKey(c => c.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
