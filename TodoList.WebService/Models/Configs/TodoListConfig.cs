using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.WebService.Models.Configs;

public class TodoListConfig: IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.ToTable("t_TodoList");
        builder.HasKey(e => e.TodoId);

        builder.Property(e => e.TodoId).HasDefaultValueSql("(newid())");
        builder.Property(e => e.InsertTime)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(e => e.Name).IsRequired();

        builder.Property(e => e.UpdateTime)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.HasOne<Employee>(d => d.InsertEmployee)
            .WithMany(p => p.TodoListInsertEmployees)
            .HasForeignKey(d => d.InsertEmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Todo_InsertEmpId");

        builder.HasOne<Employee>(d => d.UpdateEmployee)
            .WithMany(p => p.TodoListUpdateEmployees)
            .HasForeignKey(d => d.UpdateEmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Todo_UpdateEmpId");
    }
}