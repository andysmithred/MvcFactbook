using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class ShipService
    {
        #region Constructor

        public ShipService()
        {
            ShipGroupSets = new HashSet<ShipGroupSet>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string Penant { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ship Id")]
        public int ShipId { get; set; }

        [Required]
        [Display(Name = "Ship Class Id")]
        public int ShipClassId { get; set; }

        [Required]
        [Display(Name = "Ship Sub-Type Id")]
        public int ShipSubTypeId { get; set; }

        [Display(Name = "Start Service")]
        [DataType(DataType.Date)]
        public DateTime? StartService { get; set; }

        [Display(Name = "End Service")]
        [DataType(DataType.Date)]
        public DateTime? EndService { get; set; }

        [Display(Name = "Branch Id")]
        public int? BranchId { get; set; }

        public string Fate { get; set; }

        public bool Active { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public Ship Ship { get; set; }

        [Display(Name = "Ship Class")]
        public ShipClass ShipClass { get; set; }

        [Display(Name = "Ship Sub-Type")]
        public ShipSubType ShipSubType { get; set; }

        public Branch Branch { get; set; }

        public ICollection<ShipGroupSet> ShipGroupSets { get; set; }

        #endregion Foreign Properties
    }
}
