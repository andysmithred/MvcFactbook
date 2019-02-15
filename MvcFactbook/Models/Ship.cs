using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Ship
    {
        #region Constructor

        public Ship()
        {
            ShipServices = new HashSet<ShipService>();
        }
       
        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Launched { get; set; }

        [Display(Name = "Builder Id")]
        public int? BuilderId { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public Builder Builder { get; set; }

        public ICollection<ShipService> ShipServices { get; set; }

        #endregion Foreign Properties
    }
}