using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BuilderView : View<Builder>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        public int? Founded => ViewObject.Founded;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipView> Ships => GetViewList<ShipView, Ship>(ViewObject.Ships);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name;

        public string FoundedLabel => Founded.HasValue ? Founded.Value.ToString() : "--";

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}
