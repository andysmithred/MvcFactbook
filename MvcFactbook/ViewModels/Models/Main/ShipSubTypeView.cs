using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipSubTypeView : View<ShipSubType>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Type => ViewObject.Type;

        [Required]
        [Display(Name = "Ship Type Id")]
        public int ShipTypeId => ViewObject.ShipTypeId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Ship Type")]
        public ShipTypeView ShipType => GetView<ShipTypeView, ShipType>(ViewObject.ShipType);

        [Display(Name = "Ship Services")]
        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}