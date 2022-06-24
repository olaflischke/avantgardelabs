using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindDal.Model;

[Table("employeesjson")]
public partial class EmployeesJson
{
    [Key] 
    [Column("employee_id")] 
    public short EmployeeId { get; set; }
    
    [Column("data", TypeName = "jsonb")] 
    public string Data { get; set; }
}