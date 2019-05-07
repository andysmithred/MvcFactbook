using MvcFactbook.Code.Enum;
using MvcFactbook.ViewModels.Models.Main;
using System.Collections.Generic;
using System.Linq;

namespace MvcFactbook.Code.Classes
{
    public class Fleet
    {
        #region Private Declarations

        private IEnumerable<ShipServiceView> shipServicesList = null;
        private IEnumerable<ShipView> shipsList = null;

        #endregion Private Declarations

        #region Constructors

        public Fleet(IEnumerable<ShipServiceView> shipServices)
        {
            ShipServicesList = shipServices;
        }

        public Fleet(IEnumerable<ShipView> ships)
        {
            ShipsList = ships;
        }

        #endregion Conbstructors

        #region Public Properties

        #region Total

        public IEnumerable<ShipServiceView> ShipServicesList
        {
            get => shipServicesList ?? (shipServicesList = ShipsList.SelectMany(x => x.ShipServices).Distinct(x => x.Id));
            set => shipServicesList = value;
        }

        public IEnumerable<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = ShipServicesList.Select(x => x.Ship).Distinct(x => x.Id));
            set => shipsList = value;
        }

        public FleetType TotalFleet => new FleetType(ShipServicesList);

        public bool HasFleet => ShipServicesList.Count() > 0;

        #endregion Total

        #region Active

        public IEnumerable<ShipServiceView> ActiveShipServicesList => ShipServicesList.Where(x => x.Active);

        public IEnumerable<ShipView> ActiveShipsList => ActiveShipServicesList.Select(x => x.Ship).Distinct(x => x.Id);

        public FleetType ActiveFleet => new FleetType(ShipServicesList.Where(x => x.Active));

        public bool HasActiveFleet => ShipServicesList.Count() > 0;

        #endregion Active

        #region Inactive

        public IEnumerable<ShipServiceView> InactiveShipServicesList => ShipServicesList.Where(x => !x.Active);

        public IEnumerable<ShipView> InactiveShipsList => InactiveShipServicesList.Select(x => x.Ship).Distinct(x => x.Id);

        public FleetType InactiveFleet => new FleetType(ShipServicesList.Where(x => !x.Active));

        public bool HasInactiveFleet => ShipServicesList.Count() > 0;

        #endregion Inactive

        #region Get Fleet Item

        public int FleetItemId { get; set; }

        public eFleetItemListType FleetItemListType { get; set; }

        public eFleetType FleetType { get; set; }

        public FleetItem DisplayFleetItem => GetDisplayFleetItemList(FleetType, FleetItemListType).Where(x => x.Id == FleetItemId).FirstOrDefault();

        public string DisplayFleetItemLabel => DisplayFleetItem.Name.ToUpper() + " " + FleetTypeLabel;

        public string FleetTypeLabel => FleetType.ToString().ToUpper();

        public string FleetItemListTypeLabel => CommonFunctions.GetFleetItemListTypeLabel(FleetItemListType);

        #endregion Get Fleet Item

        #endregion Public Properties

        #region Methods

        private FleetType GetDisplayFleet(eFleetType fleetType)
        {
            switch (fleetType)
            {
                case eFleetType.Total:
                    return TotalFleet;
                case eFleetType.Active:
                    return ActiveFleet;
                case eFleetType.Inactive:
                    return InactiveFleet;
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
