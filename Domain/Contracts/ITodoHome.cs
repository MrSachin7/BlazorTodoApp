using Domain.Models;

namespace Domain.Contracts; 

public interface ITodoHome {
    public Task<ICollection<Todo>> GetAsync(TodoFilter? filter);

    public Task<Todo> GetById(int id);
    public Task<Todo> AddAsync(Todo todo);
    public Task<Todo> DeleteAsync(int id);
    public Task<Todo> UpdateAsync(Todo todo);  
}