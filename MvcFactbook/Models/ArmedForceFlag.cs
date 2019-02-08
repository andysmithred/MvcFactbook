using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class ArmedForceFlag
    {
        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Armed Force Id")]
        public int ArmedForceId { get; set; }

        [Required]
        [Display(Name = "Flag Id")]
        public int FlagId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? Start { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? End { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Armed Force")]
        public ArmedForce ArmedForce { get; set; }

        [Display(Name = "Flag")]
        public Flag Flag { get; set; }

        #endregion Foreign Properties
    }
}
