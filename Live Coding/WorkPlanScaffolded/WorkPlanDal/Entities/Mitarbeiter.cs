using System;
using System.Collections.Generic;

namespace WorkPlanDal.Entities
{
    public partial class Mitarbeiter
    {
        public Mitarbeiter()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
