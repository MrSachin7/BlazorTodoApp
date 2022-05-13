using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCData; 

public class TodoContext : DbContext {
    public DbSet<Todo> Todos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(@"Data Source =D:\SEM 3\SEM3\DNP1\BlazorTodoApp\EFCData\Todo.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Todo>().HasKey(todo => todo.Id);
    }
    public void Seed()
    {
        if (Todos.Any()) return;

        Todo[] ts =
        {
            new Todo(1, "Dishes"),
            new Todo(1, "Walk the dog"),
            new Todo(2, "Do DNP homework"),
            new Todo(3, "Eat breakfast"),
            new Todo(4, "Mow lawn"),
        };
        Todos.AddRange(ts);
        SaveChanges();
    }
}