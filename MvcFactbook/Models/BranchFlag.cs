using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class BranchFlag
    {
        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

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

        public Branch Branch { get; set; }
        public Flag Flag { get; set; }

        #endregion Foreign Properties
    }
}