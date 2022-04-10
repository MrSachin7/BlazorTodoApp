using System.Text.Json;
using Domain.Models;

namespace FileData.DataAccess;

public class FileContext {
    private string todoFilePath = "todos.json";
    private ICollection<Todo> todos;

    public ICollection<Todo> Todos {
        get {
            if (todos == null) {
                LoadData();
            }

            return todos;
        } 
        
    }

   
    public FileContext() {
        if (!File.Exists(todoFilePath)) {
            Seed();
        }
    }

    private void Seed() {
        Todo[] ts = {
            new Todo(1, "Dishes") {
                Id = 1,
            },
            new Todo(1, "Walk the dog") {
                Id = 1,
            },
            new Todo(2, "Do DNP homework") {
                Id = 3,
            },
            new Todo(3, "Eat breakfast") {
                Id = 4,
            },
            new Todo(4, "Mow lawn") {
                Id = 5,
            },
        };
        todos = ts.ToList();
        SaveChanges();
    }

    public void SaveChanges() {
        string serialize = JsonSerializer.Serialize(Todos, new JsonSerializerOptions() {
            WriteIndented = true
        });
        File.WriteAllText(todoFilePath,serialize);
        todos = null;
    }
    private void LoadData() {
        string content = File.ReadAllText(todoFilePath);
        todos = JsonSerializer.Deserialize<List<Todo>>(content);

    }

}