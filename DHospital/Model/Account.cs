using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHospital.Model
{
    public class Account
    {
        [Key]        
        public int Ac_id { get; set; }
        public int Pat_id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Number { get; set; }
        [MaxLength(255)]
        public string Sex { get; set; }
        [MaxLength(255)]
        public string Age  { get; set; }
        public DateTime RDate  { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string Pay { get; set; }
        [MaxLength(255)]
        public string Other { get; set; }
        [MaxLength(255)]
        public string Message { get; set; }
        [MaxLength(255)]
        public string RTime { get; set; }
    }
}
