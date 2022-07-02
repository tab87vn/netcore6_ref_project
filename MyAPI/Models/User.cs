using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Column("UserId")]
    [Required(ErrorMessage = "Missing Id")]
    [MaxLength(40, ErrorMessage = "Meh")]
    public int Id { get; set; }
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"My name is {Name}";
    }
}