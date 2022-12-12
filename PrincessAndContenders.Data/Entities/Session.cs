using System.ComponentModel.DataAnnotations.Schema;

namespace PrincessAndContenders.Data;

public record Session : Entity
{
    public int SessionId { get; init; }
    [ForeignKey("AttemptId")]
    public Attempt Attempt { get; init; }
    public int NextContenderId { get; set; }
}