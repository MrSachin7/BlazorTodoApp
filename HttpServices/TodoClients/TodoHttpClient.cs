using System.ComponentModel.Design;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Contracts;
using Domain.Models;

namespace HttpServices.TodoClients;

public class TodoHttpClient : ITodoHome {
    public async Task<ICollection<Todo>> GetAsync() {
        using HttpClient client = new();
        HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7162/todos");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode}");
        }

        ICollection<Todo> todos = JsonSerializer.Deserialize<ICollection<Todo>>(content, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        })!;
        return todos;
    }

    public async Task<Todo> GetById(int id) {
        using HttpClient client = new();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7162/todos/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode}, {content}");
        }

        Todo todo = JsonSerializer.Deserialize<Todo>(content, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        })!;
        return todo;
    }

    public async Task<Todo> AddAsync(Todo todo) {
        using HttpClient client = new();
        string todoAsJson = JsonSerializer.Serialize(todo);
        StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://localhost:7162/Todos", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error : {response.StatusCode} , {responseContent}");
        }

        Todo returned = JsonSerializer.Deserialize<Todo>(responseContent, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        })!;
        return returned;
    }

    public async Task<Todo> DeleteAsync(int id) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:7162/todos/{id}");
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode}, {responseContent}");
        }

        Todo returned = JsonSerializer.Deserialize<Todo>(responseContent, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return returned;
    }

    public async Task<Todo> UpdateAsync(Todo todo) {
        using HttpClient client = new HttpClient();
        string todoAsJson = JsonSerializer.Serialize(todo);
        StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PatchAsync($"https://localhost:7162/todos", content);
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode}, {responseContent}");
        }

        Todo returned = JsonSerializer.Deserialize<Todo>(responseContent, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return returned;
    }
}