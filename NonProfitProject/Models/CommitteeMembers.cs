using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class CommitteeMembers
    {
        //holds committee member table information
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitteeMbrID { get; set; }
        public int CommitteeID { get; set; }
        public Committees committee { get; set; }
        public string EmpID { get; set;}
        public Employees employee { get; set; }
        [Required(ErrorMessage = "Please assign new member to a position")]
        public string CommitteePosition { get; set; }
    }
}
