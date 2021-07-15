using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models
{
    public class SavedPaymentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SavedPaymentID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set;}
        public string CardNumber { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime ExpDate { get; set; }
        public string CVV { get; set; }
        public int Last4Digits { get; set; }
    }
}
