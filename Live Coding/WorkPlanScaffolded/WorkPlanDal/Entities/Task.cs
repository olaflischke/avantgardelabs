using System;
using System.Collections.Generic;

namespace WorkPlanDal.Entities
{
    public partial class Task
    {
        public Task()
        {
            Workers = new HashSet<Mitarbeiter>();
        }

        public int Id { get; set; }
        public string Bezeichnung { get; set; } = null!;

        public virtual ICollection<Mitarbeiter> Workers { get; set; }
    }
}
