using System.ComponentModel.DataAnnotations.Schema;

namespace WorkplanDal;

public class Person
{
    public string Name { get; set; }
    public List<Task> Tasks { get; set; } = new();
    public Guid Id { get; set; } = Guid.NewGuid();

    [NotMapped]
    public string PKZ { get; set; }
}