using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoList.WebService.Models;

public class TodoItemDbContextDesignFactory: IDesignTimeDbContextFactory<TodoContext>
{
    public TodoContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<TodoContext> builder = new DbContextOptionsBuilder<TodoContext>();
        string connStr = Environment.GetEnvironmentVariable("ConnectionStrings:Demo3")!;
        builder.UseSqlServer(connStr!);
        return new TodoContext(builder.Options);
    }
}