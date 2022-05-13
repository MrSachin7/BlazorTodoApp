namespace Domain.Models; 

public class TodoFilter {
    public int? UserId { get; set; }
    public bool? IsCompleted { get; set; }
}