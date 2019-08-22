namespace MvvmLightTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key]
        public int EmpNo { get; set; }

        [StringLength(50)]
        public string EmpName { get; set; }

        public double? Salary { get; set; }

        [StringLength(50)]
        public string DeptName { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }
    }
}
