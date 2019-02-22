using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class SucceedingClassView : View<SucceedingClass>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Preceding Class Id")]
        public int ShipClassId => ViewObject.ShipClassId;

        [Required]
        [Display(Name = "Succeeding Class Id")]
        public int SucceedingClassId => ViewObject.SucceedingClassId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Preceding Class")]
        public ShipClassView PrecedingShipClass => GetView<ShipClassView, ShipClass>(ViewObject.PrecedingShipClass);

        [Display(Name = "Succeeding Class")]
        public ShipClassView SucceedingShipClass => GetView<ShipClassView, ShipClass>(ViewObject.SucceedingShipClass);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PrecedingShipClass.Name + ":" + SucceedingShipClass.Name;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}
