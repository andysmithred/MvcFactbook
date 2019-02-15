using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipView : View<Ship>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        [DataType(DataType.Date)]
        public DateTime? Launched => ViewObject.Launched;

        [Display(Name = "Builder Id")]
        public int? BuilderId => ViewObject.BuilderId;

        #endregion Database Properties

        #region Foreign Properties

        public BuilderView Builder => GetView<BuilderView, Builder>(ViewObject.Builder);

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Year;

        public string Year => Launched.HasValue ? Launched.Value.Year.ToString() : "--";

        public string LaunchedLabel => Launched.HasValue ? Launched.Value.ToString("dd MMMM yyyy") : "--";

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}
