using MvcFactbook.Models;
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
        public int ShipTypeId => ViewObject.ShipTypeId;

        #endregion Database Properties

        #region Foreign Properties

        public ShipTypeView ShipType => GetView<ShipTypeView, ShipType>(ViewObject.ShipType);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type + ":" + ShipType.Type;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}