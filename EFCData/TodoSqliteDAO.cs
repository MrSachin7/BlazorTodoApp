using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCData;

public class TodoSqliteDAO : ITodoHome {
    private readonly TodoContext _context;

    public TodoSqliteDAO(TodoContext context) {
        _context = context;
    }

    public async Task<ICollection<Todo>> GetAsync(TodoFilter? filter) {
        IQueryable<Todo> todos = _context.Todos.AsQueryable();
        if (filter.UserId !=null) {
            todos = todos.Where(todo => todo.OwnerId == filter.UserId);
        }

        if (filter.IsCompleted !=null) {
            todos = todos.Where(todo => todo.IsCompleted == filter.IsCompleted);

        }

        ICollection<Todo> result = await todos.ToListAsync();
        return result;
    }

    public Task<Todo> GetById(int id) {
        throw new NotImplementedException();
    }

    public async Task<Todo> AddAsync(Todo todo) {
        EntityEntry<Todo> added = await _context.AddAsync(todo);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Todo> DeleteAsync(int id) {
        Todo? existing = await _context.Todos.FindAsync(id);
        if (existing is null) {
            throw new Exception($"Could not find the Todo with id {id}. Nothing was deleted");
        }

        _context.Todos.Remove(existing);
        await _context.SaveChangesAsync();
        return existing;
    }


    public async Task<Todo> UpdateAsync(Todo todo) {
        EntityEntry<Todo> entityEntry = _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }
}