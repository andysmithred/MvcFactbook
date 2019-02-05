using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Flag
    {
        #region Constructor

        public Flag()
        {
            ArmedForceFlags = new HashSet<ArmedForceFlag>();
            BranchFlags = new HashSet<BranchFlag>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(6)]
        [Required]
        public string Code { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool Active { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ArmedForceFlag> ArmedForceFlags { get; set; }

        public ICollection<BranchFlag> BranchFlags { get; set; }

        #endregion Foreign Properties
    }
}