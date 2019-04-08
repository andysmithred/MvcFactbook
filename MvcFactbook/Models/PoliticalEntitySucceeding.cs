using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class PoliticalEntitySucceeding
    {
        #region Constructor

        public PoliticalEntitySucceeding() { }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Preceding Entity Id")]
        public int PoliticalEntityId { get; set; }

        [Required]
        [Display(Name = "Succeeding Entity Id")]
        public int SucceedingPoliticalEntityId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Preceding Entity")]
        public PoliticalEntity PrecedingPoliticalEntity { get; set; }

        [Display(Name = "Succeeding Entity")]
        public PoliticalEntity SucceedingPoliticalEntity { get; set; }

        #endregion Foreign Properties
    }
}
