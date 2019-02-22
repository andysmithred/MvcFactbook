using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class SucceedingClass
    {
        #region Constructor

        public SucceedingClass() { }
        
        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Preceding Class")]
        public int ShipClassId { get; set; }

        [Required]
        [Display(Name = "Succeeding Class")]
        public int SucceedingClassId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Preceding Class")]
        public ShipClass PrecedingShipClass { get; set; }

        [Display(Name = "Succeeding Class")]
        public ShipClass SucceedingShipClass { get; set; }

        #endregion Foreign Properties
    }
}
