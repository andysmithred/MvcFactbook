using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Models
{
    public partial class ArmedForce
    {
        public ArmedForce()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(5)]
        [Required]
        public string Code { get; set; }

        [Required]
        public bool IsActive { get; set; }


        public long? Budget { get; set; }
    }
}
