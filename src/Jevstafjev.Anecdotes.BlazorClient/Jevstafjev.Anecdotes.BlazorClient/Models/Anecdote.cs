namespace Jevstafjev.Anecdotes.BlazorClient.Models;

public class Anecdote
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public List<Tag> Tags { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
