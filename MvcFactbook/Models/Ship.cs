using MvcFactbook.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Ship: IComplete
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
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Launched")]
        public DateTime? Launched { get; set; }

        [Display(Name = "Dockyard Id")]
        public int? DockyardId { get; set; }

        [Required]
        [Display(Name = "Complete")]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Dockyard")]
        public Dockyard Dockyard { get; set; }

        public ICollection<ShipService> ShipServices { get; set; }

        #endregion Foreign Properties
    }
}