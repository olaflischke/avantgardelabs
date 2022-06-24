using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using WorkplanDal;
using Task = WorkplanDal.Task;

namespace WorkplanTests;

public class Tests
{
    private DbContextOptions options;
    
    [SetUp]
    public void Setup()
    {
        options = new DbContextOptionsBuilder<WorkPlanContext>()
            .UseNpgsql("server=localhost;port=5432;database=WorkPlanDb;user id=demo;password=Geheim123")
            .LogTo(log => LogIt(log))
            .Options;

        WorkPlanContext context = new(options);

        context.Database.EnsureCreated();
    }

    [Test]
    public void NzuMZuordnung()
    {
        Task entladen = new Task() { Bezeichnung = "LKW entladen" };
        Task kisten = new Task() { Bezeichnung = "Kisten stapeln" };

        Person klaus = new Person() { Name = "Klaus" };
        Person theo = new Person() { Name = "Theo" };
        
        entladen.Workers.Add(klaus);
        entladen.Workers.Add(theo);
        
        kisten.Workers.Add(klaus);
        
        klaus.Tasks.Add(entladen);
        klaus.Tasks.Add(kisten);
        
        theo.Tasks.Add(entladen);

        using WorkPlanContext context = new WorkPlanContext(options);
        context.Tasks.Add(entladen);
        context.Tasks.Add(kisten);

        context.SaveChanges();
        
        Assert.Pass();
    }
    
    private void LogIt(string logString)
    {
        Console.WriteLine(logString);
    }

}