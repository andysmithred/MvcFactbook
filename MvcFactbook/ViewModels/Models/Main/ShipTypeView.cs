using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipTypeView : View<ShipType>
    {
        #region Private Declarations

        private Fleet fleet = null;

        #endregion Private Declarations

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

        public ICollection<ShipServiceView> ShipServices => ShipSubTypes.SelectMany(x => x.ShipServices).Distinct(x => x.Id).ToList();

        public ICollection<BranchView> Branches => ShipServices.Select(x => x.Branch).Distinct(x => x.Id).ToList();

        public Fleet Fleet => fleet ?? (fleet = new Fleet(ShipServices));

        #endregion Other Properties

    }
}