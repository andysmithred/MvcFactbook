using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class ShipClass: IComplete
    {
        #region Constructor

        public ShipClass()
        {
            ShipServices = new HashSet<ShipService>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Sub-class")]
        public string SubClassName { get; set; }

        public int? Displacement { get; set; }

        public double? Length { get; set; }

        public double? Beam { get; set; }

        public int? Propulsion { get; set; }

        public double? Speed { get; set; }

        public int? Crew { get; set; }

        public int? Year { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipService> ShipServices { get; set; }

        public ICollection<SucceedingClass> PrecedingClasses { get; set; }

        public ICollection<SucceedingClass> SucceedingClasses { get; set; }

        #endregion Foreign Properties
    }
}