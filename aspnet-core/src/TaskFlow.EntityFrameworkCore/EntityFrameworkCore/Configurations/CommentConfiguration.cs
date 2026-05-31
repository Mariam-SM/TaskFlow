using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Entities.Comments;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TaskFlow.EntityFrameworkCore.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ConfigureByConvention();

            builder.ToTable("Comments");

            builder.Property(c => c.Content)
                   .IsRequired()
                   .HasMaxLength(TaskFlowConsts.MaxContentLength);


        }
    }
}
