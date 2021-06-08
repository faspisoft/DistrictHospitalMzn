using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHospital.Model
{
    public class Company
    {
        [Key]
        public int C_id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string FirmPeriodName { get; set; }

        public DateTime StartFrom { get; set; }
        public DateTime EndAt { get; set; }

        [MaxLength(255)]
        public string Cst_No { get; set; }
        [MaxLength(255)]
        public string Tin_No { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Address1 { get; set; }
        [MaxLength(255)]
        public string Address2 { get; set; }
        [MaxLength(255)]
        public string ContactNo { get; set; }
    }
}
