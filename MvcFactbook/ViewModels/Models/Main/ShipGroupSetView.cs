using MvcFactbook.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipGroupSetView : View<ShipGroupSet>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Ship Service Id")]
        public int ShipServiceId => ViewObject.ShipServiceId;

        [Required]
        [Display(Name = "Ship Group Id")]
        public int ShipGroupId => ViewObject.ShipGroupId;

        #endregion Database Properties

        #region Foreign Properties

        public ShipServiceView ShipService => GetView<ShipServiceView, ShipService>(ViewObject.ShipService);
        public ShipGroupView ShipGroup => GetView<ShipGroupView, ShipGroup>(ViewObject.ShipGroup);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => ShipService.Name + ":" + ShipGroup.Name;

        #endregion Other Properties
    }
}
