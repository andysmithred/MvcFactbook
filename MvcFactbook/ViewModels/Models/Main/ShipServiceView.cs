using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipServiceView : View<ShipService>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(10)]
        public string Penant => ViewObject.Penant;

        [Required]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Ship Id")]
        public int ShipId => ViewObject.ShipId;

        [Required]
        [Display(Name = "Ship Class Id")]
        public int ShipClassId => ViewObject.ShipClassId;

        [Required]
        [Display(Name = "Ship Sub-Type Id")]
        public int ShipSubTypeId => ViewObject.ShipSubTypeId;

        [DataType(DataType.Date)]
        [Display(Name = "Start Service")]
        public DateTime? StartService => ViewObject.StartService;

        [DataType(DataType.Date)]
        [Display(Name = "End Service")]
        public DateTime? EndService => ViewObject.EndService;

        [Display(Name = "Branch Id")]
        public int? BranchId => ViewObject.BranchId;

        public string Fate => ViewObject.Fate;

        [Required]
        public bool Active => ViewObject.Active;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Ship")]
        public ShipView Ship => GetView<ShipView, Ship>(ViewObject.Ship);

        [Display(Name = "Ship Class")]
        public ShipClassView ShipClass => GetView<ShipClassView, ShipClass>(ViewObject.ShipClass);

        [Display(Name = "Ship Sub-Type")]
        public ShipSubTypeView ShipSubType => GetView<ShipSubTypeView, ShipSubType>(ViewObject.ShipSubType);

        [Display(Name = "Branch")]
        public BranchView Branch => GetView<BranchView, Branch>(ViewObject.Branch);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Penant;

        public bool HasFlag => Branch == null ? Branch.HasFlag : false;

        public FlagView Flag => HasFlag ? Branch.CurrentFlag : null;

        public string ImageSource => Flag?.ImageSource;

        #endregion Other Properties
    }
}