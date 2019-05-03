using MvcFactbook.Code.Classes;
using MvcFactbook.Code.Enum;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipSubTypeView : View<ShipSubType>
    {
        #region Private Variables

        private Fleet totalFleet = null;
        private Fleet activeFleet = null;
        private Fleet inactiveFleet = null;

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

        public ICollection<ShipServiceView> ActiveServices => ShipServices.Where(x => x.Active).ToList();

        public ICollection<ShipServiceView> InactiveServices => ShipServices.Where(x => !x.Active).ToList();

        public bool HasFleet => ShipServices.Count > 0;

        public Fleet TotalFleet => totalFleet ?? (totalFleet = new Fleet(ShipServices));

        public Fleet ActiveFleet => activeFleet ?? (activeFleet = new Fleet(ActiveServices));

        public Fleet InactiveFleet => inactiveFleet ?? (inactiveFleet = new Fleet(InactiveServices));


        public int FleetItemId { get; set; }

        public eFleetItemListType FleetItemListType { get; set; }

        public eFleetType FleetType { get; set; }

        public FleetItem DisplayFleetItem => GetDisplayFleetItemList(FleetType, FleetItemListType).Where(x => x.Id == FleetItemId).FirstOrDefault();

        public string DisplayFleetItemLabel => DisplayFleetItem.Name.ToUpper() + " " + FleetTypeLabel;

        public string FleetTypeLabel => FleetType.ToString().ToUpper();

        public string FleetItemListTypeLabel => CommonFunctions.GetFleetItemListTypeLabel(FleetItemListType);

        #endregion Other Properties

        #region Methods

        private Fleet GetDisplayFleet(eFleetType fleetType)
        {
            switch (fleetType)
            {
                case eFleetType.Total:
                    return TotalFleet;
                case eFleetType.Active:
                    return ActiveFleet;
                case eFleetType.Inactive:
                    return inactiveFleet;
                default:
                    return TotalFleet;
            }
        }

        private IEnumerable<FleetItem> GetDisplayFleetItemList(eFleetType fleetType, eFleetItemListType fleetItemListType)
        {
            switch (fleetItemListType)
            {
                case eFleetItemListType.ByCategory:
                    return GetDisplayFleet(fleetType).FleetServicesByShipCategory;
                case eFleetItemListType.ByType:
                    return GetDisplayFleet(fleetType).FleetServicesByShipType;
                case eFleetItemListType.BySubType:
                    return GetDisplayFleet(fleetType).FleetServicesByShipSubType;
                case eFleetItemListType.ByClass:
                    return GetDisplayFleet(fleetType).FleetServicesByShipClass;
                case eFleetItemListType.ByBranch:
                    return GetDisplayFleet(fleetType).FleetServicesByBranch;
                case eFleetItemListType.ByBuilder:
                    return GetDisplayFleet(fleetType).FleetServicesByBranch; //TODO: fix
                default:
                    return GetDisplayFleet(fleetType).FleetServicesByShipCategory;
            }
        }

        #endregion Methods
    }
}