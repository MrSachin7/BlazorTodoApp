using Domain.Models;

namespace Domain.Contracts; 

public interface ITodoHome {
    public Task<ICollection<Todo>> GetAsync();
    public Task<Todo> GetById(int id);
    public Task<Todo> AddAsync(Todo todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Todo todo);  
}