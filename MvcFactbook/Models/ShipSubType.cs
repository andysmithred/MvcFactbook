using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class ShipSubType
    {
        #region Constructor

        public ShipSubType()
        {
            ShipServices = new HashSet<ShipService>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Ship Type Id")]
        public int ShipTypeId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Ship Type")]
        public ShipType ShipType { get; set; }

        [Display(Name = "Ship Services")]
        public ICollection<ShipService> ShipServices { get; set; }

        #endregion Foreign Properties
    }
}