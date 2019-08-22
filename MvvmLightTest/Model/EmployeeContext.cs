namespace MvvmLightTest.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext()
            : base("name=EmployeeModel")
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
