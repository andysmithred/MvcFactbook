using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipCategoryView: View<ShipCategory>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Category => ViewObject.Category;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipTypeView> ShipTypes => GetViewList<ShipTypeView, ShipType>(ViewObject.ShipTypes);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Category;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}