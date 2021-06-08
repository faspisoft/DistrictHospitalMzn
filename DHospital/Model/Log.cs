using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHospital.Model
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public int RegNo { get; set; }
        public DateTime RDate { get; set; }
        [MaxLength(255)]
        public string RTime { get; set; }
        [MaxLength(255)]
        public string OldName { get; set; }
        [MaxLength(255)]
        public string NewName { get; set; }
    }
}
