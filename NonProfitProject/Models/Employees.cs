using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmpID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Position { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool FinishedAccountSetup { get; set; }


        public CommitteeMembers CommitteeMembers { get; set; }
    }
}
