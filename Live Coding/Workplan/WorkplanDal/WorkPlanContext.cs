using Microsoft.EntityFrameworkCore;
using Npgsql.Internal.TypeHandlers.FullTextSearchHandlers;

namespace WorkplanDal;

public class WorkPlanContext:DbContext
{
    public WorkPlanContext(DbContextOptions options):base(options)
    {
        
    }
    
    public DbSet<Person> Mitarbeiter { get; set; }
    public DbSet<Task> Tasks { get; set; }
}