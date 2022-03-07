using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Todo {
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int OwnerId { get; set; }

[Required, MaxLength(128)]
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int ownerId, string title) {
        OwnerId = ownerId;
        Title = title;
    }

    public Todo() {

    }
}