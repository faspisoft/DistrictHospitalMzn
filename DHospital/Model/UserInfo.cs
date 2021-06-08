using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHospital.Model
{
    public class UserInfo
    {
        [Key]
        public int U_id { get; set; }
        [MaxLength(255)]
        public string UName { get; set; }
        [MaxLength(255)]
        public string UPass { get; set; }
        [MaxLength(255)]
        public string UType { get; set; }
        [MaxLength(255)]
        public string UPass2 { get; set; }
    }
}
