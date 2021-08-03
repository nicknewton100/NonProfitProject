using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NonProfitProject.Models
{
    public class Committees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitteesID { get; set; }
        [Required(ErrorMessage = "Please assign committee name")]
        public string CommitteeName { get; set; }
        [Required(ErrorMessage = "Please assign committee description")]
        public string CommitteeDescription { get; set; }
        public DateTime? CommitteeCreationDate { get; set; }
        public ICollection<CommitteeMembers> committeeMembers { get; set; }
    }
}
