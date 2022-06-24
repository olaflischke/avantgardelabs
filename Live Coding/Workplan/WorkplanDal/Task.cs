namespace WorkplanDal;

public class Task
{
    public int Id { get; set; }
    public string Bezeichnung { get; set; }
    public List<Person> Workers { get; set; } = new();
}