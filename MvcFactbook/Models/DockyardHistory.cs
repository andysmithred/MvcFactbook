using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class DockyardHistory
    {
        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dockyard Id")]
        public int DockyardId { get; set; }

        [Required]
        [Display(Name = "Shipbuilder Id")]
        public int ShipbuilderId { get; set; }

        public int? Start { get; set; }

        public int? End { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Dockyard")]
        public Dockyard Dockyard { get; set; }

        [Display(Name = "Shipbuilder")]
        public Shipbuilder Shipbuilder { get; set; }

        #endregion Foreign Properties
    }
}
