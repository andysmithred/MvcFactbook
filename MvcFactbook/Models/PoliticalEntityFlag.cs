using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class PoliticalEntityFlag
    {
        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId { get; set; }

        [Required]
        [Display(Name = "Flag Id")]
        public int FlagId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntity PoliticalEntity { get; set; }

        [Display(Name = "Flag")]
        public Flag Flag { get; set; }

        #endregion Foreign Properties
    }
}
