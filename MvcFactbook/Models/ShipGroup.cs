using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public class ShipGroup
    {
        #region Constructor

        public ShipGroup()
        {
            ShipGroupSets = new HashSet<ShipGroupSet>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(20)]
        public string Icon { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipGroupSet> ShipGroupSets { get; set; }

        #endregion Foreign Properties
    }
}
