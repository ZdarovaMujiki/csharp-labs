using System.ComponentModel.DataAnnotations.Schema;

namespace PrincessAndContenders.Data;

public record Attempt : Entity
{
    public string InternalContenders { get; set; }
    
    [NotMapped]
    public Contender[] Contenders
    {
        get => Array.ConvertAll(InternalContenders.Split(';'), Contender.Parse);
        set => InternalContenders = string.Join(";", value.Select(contender => contender.ToString()).ToArray());
    }
}