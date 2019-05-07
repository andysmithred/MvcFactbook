using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipSubTypeView : View<ShipSubType>
    {
        #region Private Variables

        private Fleet fleet = null;

        #endregion Private Variables

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

        public ICollection<BranchView> Branches => ShipServices.Select(x => x.Branch).Distinct(x => x.Id).ToList();

        //public ICollection<ShipServiceView> ActiveServices => ShipServices.Where(x => x.Active).ToList();

        //public ICollection<ShipServiceView> InactiveServices => ShipServices.Where(x => !x.Active).ToList();

        //public bool HasFleet => ShipServices.Count > 0;

        public Fleet Fleet => fleet ?? (fleet = new Fleet(ShipServices));

        #endregion Other Properties
    }
}