using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Entities.Projects;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TaskFlow.EntityFrameworkCore.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable("Projects");

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(TaskFlowConsts.MaxtNameLength);

            builder.Property(p => p.Description)
                   .HasMaxLength(TaskFlowConsts.MaxDefaultDescriptionLength);

            builder.HasMany(p => p.Tasks)
                   .WithOne(t => t.Project)
                   .HasForeignKey(t => t.ProjectId);
        }
    }
}
