using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TodoList.WebService.Models
{
    public partial class TodoContext : DbContext
    {

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<TodoList> TodoLists { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Division> Divisions { get; set; } = null!;
        public DbSet<JobTitle> JobTitles { get; set; } = null!;

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 從當前運行的Assembly加載所有的IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
