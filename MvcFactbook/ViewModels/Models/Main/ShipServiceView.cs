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
        public int ShipId => ViewObject.ShipId;

        [Required]
        public int ShipClassId => ViewObject.ShipClassId;

        [Required]
        public int ShipSubTypeId => ViewObject.ShipSubTypeId;

        [Display(Name = "Start Service")]
        [DataType(DataType.Date)]
        public DateTime? StartService => ViewObject.StartService;

        [Display(Name = "End Service")]
        [DataType(DataType.Date)]
        public DateTime? EndService => ViewObject.EndService;

        public int? BranchId => ViewObject.BranchId;

        public string Fate => ViewObject.Fate;

        [Required]
        public bool Active => ViewObject.Active;
        
        #endregion Database Properties

        #region Foreign Properties

        public ShipView Ship => GetView<ShipView, Ship>(ViewObject.Ship);

        public ShipClassView ShipClass => GetView<ShipClassView, ShipClass>(ViewObject.ShipClass);

        public ShipSubTypeView ShipSubType => GetView<ShipSubTypeView, ShipSubType>(ViewObject.ShipSubType);

        public BranchView Branch => GetView<BranchView, Branch>(ViewObject.Branch);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Ship.Name;

        #endregion Other Properties
    }
}