using MvcFactbook.Code.Classes;
using MvcFactbook.Code.Enum;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BranchView : View<Branch>
    {
        #region Private Variables

        private Fleet totalFleet = null;
        private Fleet activeFleet = null;
        private Fleet inactiveFleet = null;

        #endregion Private Variables

        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Armed Force")]
        public int ArmedForceId => ViewObject.ArmedForceId;

        [Required]
        [Display(Name = "Branch Type")]
        public int BranchTypeId => ViewObject.BranchTypeId;

        [Required]
        [Display(Name = "Emblem")]
        public bool HasEmblem => ViewObject.HasEmblem;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Branch Type")]
        public BranchTypeView BranchType => GetView<BranchTypeView, BranchType>(ViewObject.BranchType);

        [Display(Name = "Armed Force")]
        public ArmedForceView ArmedForce => GetView<ArmedForceView, ArmedForce>(ViewObject.ArmedForce);

        public ICollection<BranchFlagView> BranchFlags => GetViewList<BranchFlagView, BranchFlag>(ViewObject.BranchFlags);

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + ArmedForce?.Name;

        public bool HasFlag => Flags.Count > 0;

        public ICollection<FlagView> Flags => BranchFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        public FlagView CurrentFlag => Flags.OrderByDescending(x => x.StartDate).FirstOrDefault();

        public string ImageSource => CurrentFlag?.ImageSource;

        public string Image => CurrentFlag?.Image;




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

        public string FleetItemListTypeLabel => GetFleetItemListTypeLabel(FleetItemListType);

        #endregion Other Properties

        #region Methods

        public FlagView GetFlagByDate(DateTime date)
        {
            if(BranchFlags.Count > 0)
            {
                foreach (var item in BranchFlags)
                {
                    if(item.AbsoluteStart <= date && date <= item.AbsoluteEnd)
                    {
                        return item.Flag;
                    }
                }

                //If we get here return null
                return null;
            }
            else
            {
                return null;
            }
        }

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

        private string GetFleetItemListTypeLabel(eFleetItemListType fleetItemListType)
        {
            switch (fleetItemListType)
            {
                case eFleetItemListType.ByCategory:
                    return "By Category".ToUpper();
                case eFleetItemListType.ByType:
                    return "By Type".ToUpper();
                case eFleetItemListType.BySubType:
                    return "By Sub-Type".ToUpper();
                case eFleetItemListType.ByClass:
                    return "By Ship Class".ToUpper();
                case eFleetItemListType.ByBranch:
                    return "By Branch".ToUpper();
                case eFleetItemListType.ByBuilder:
                    return "By Builder".ToUpper();
                default:
                    return string.Empty;
            }
        }

        #endregion Methods
    }
}