using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public class ShipGroupSet
    {
        #region Constructor

        public ShipGroupSet() { }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public int ShipServiceId { get; set; }

        [Required]
        public int ShipGroupId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ShipService ShipService { get; set; }
        public ShipGroup ShipGroup { get; set; }

        #endregion Foreign Properties
    }
}
