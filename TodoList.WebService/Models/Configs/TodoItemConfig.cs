using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.WebService.Models.Configs;

public class TodoItemConfig: IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("t_TodoItem");
        builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
    }
}