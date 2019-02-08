using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipTypeView : View<ShipType>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Type => ViewObject.Type;

        [Required]
        [Display(Name = "Ship Category Id")]
        public int ShipCategoryId => ViewObject.ShipCategoryId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Ship Category")]
        public ShipCategoryView ShipCategory => GetView<ShipCategoryView, ShipCategory>(ViewObject.ShipCategory);

        [Display(Name = "Ship Sub-Types")]
        public ICollection<ShipSubTypeView> ShipSubTypes => GetViewList<ShipSubTypeView, ShipSubType>(ViewObject.ShipSubTypes);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type + ":" + ShipCategory?.Category;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}