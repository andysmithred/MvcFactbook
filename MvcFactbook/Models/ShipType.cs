using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class ShipType
    {
        #region Constructor

        public ShipType()
        {
            ShipSubTypes = new HashSet<ShipSubType>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }

        [Required]
        public int ShipCategoryId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ShipCategory ShipCategory { get; set; }

        public ICollection<ShipSubType> ShipSubTypes { get; set; }

        #endregion Foreign Properties
    }
}