using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class PoliticalEntityDockyard
    {
        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId { get; set; }

        [Required]
        [Display(Name = "Dockyard Id")]
        public int DockyardId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntity PoliticalEntity { get; set; }

        [Display(Name = "Dockyard")]
        public Dockyard Dockyard { get; set; }

        #endregion Foreign Properties
    }
}
