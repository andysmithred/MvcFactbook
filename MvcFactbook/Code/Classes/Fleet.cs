using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Classes
{
    public class Fleet
    {
        #region Private Variables

        private FactbookContext context = null;

        private IEnumerable<ShipServiceView> servicesList = null;
        private IEnumerable<ShipView> shipsList = null;

        private IEnumerable<ShipCategoryView> shipCategoriesList = null;
        private IEnumerable<ShipTypeView> shipTypesList = null;
        private IEnumerable<ShipSubTypeView> shipSubTypesList = null;

        private IEnumerable<ShipClassView> shipClassesList = null;
        private IEnumerable<BranchView> branchesList = null;

        private IEnumerable<BuilderView> buildersList = null;

        #endregion Private Variables

        #region Constructor

        public Fleet(IEnumerable<ShipServiceView> services)
        {
            ServicesList = services;
        }

        public Fleet(IEnumerable<ShipView> ships)
        {
            ShipsList = ships;        
        }

        #endregion Contructor

        #region Public Properties

        public FactbookContext Context
        {
            get => context ?? (context = new FactbookContext());
            set => context = value;
        }

        public IEnumerable<ShipServiceView> ServicesList
        {
            get => servicesList ?? (servicesList = ShipsList.SelectMany(x => x.ShipServices).Distinct(x => x.Id));
            set => servicesList = value;
        }

        public IEnumerable<ShipView> ShipsList
        {
            get => shipsList ?? (ShipsList = ServicesList.Select(x => x.Ship).Distinct(x => x.Id));
            set => shipsList = value;
        }

        public IEnumerable<ShipCategoryView> ShipCategoriesList
        {
            get => shipCategoriesList ?? (shipCategoriesList = new DataAccess<ShipCategory, ShipCategoryView>(Context, Context.ShipCategory).GetViews());
            set => shipCategoriesList = value;
        }

        public IEnumerable<ShipTypeView> ShipTypesList
        {
            get => shipTypesList ?? (shipTypesList = new DataAccess<ShipType, ShipTypeView>(Context, Context.ShipType).GetViews());
            set => shipTypesList = value;
        }

        public IEnumerable<ShipSubTypeView> ShipSubTypesList
        {
            get => shipSubTypesList ?? (shipSubTypesList = new DataAccess<ShipSubType, ShipSubTypeView>(Context, Context.ShipSubType).GetViews());
            set => shipSubTypesList = value;
        }

        public IEnumerable<ShipClassView> ShipClassesList
        {
            get => shipClassesList ?? (shipClassesList = new DataAccess<ShipClass, ShipClassView>(Context, Context.ShipClass).GetViews());
            set => shipClassesList = value;
        }

        public IEnumerable<BranchView> BranchesList
        {
            get => branchesList ?? (branchesList = new DataAccess<Branch, BranchView>(Context, Context.Branch).GetViews());
            set => branchesList = value;
        }

        public IEnumerable<FleetItem> FleetServicesByShipCategory => GetFleetServicesByShipCategory();

        public IEnumerable<FleetItem> FleetServicesByShipType => GetFleetServicesByShipType();

        public IEnumerable<FleetItem> FleetServicesByShipSubType => GetFleetServicesByShipSubType();

        public IEnumerable<FleetItem> FleetServicesByShipClass => GetFleetServicesByShipClass();

        public IEnumerable<FleetItem> FleetServicesByBranch => GetFleetServicesByBranch();

        public FleetItem FleetServicesTotal => new FleetItem("Total", ServicesList);


        #endregion Public Properties

        #region Methods

        private IEnumerable<FleetItem> GetFleetServicesByShipCategory()
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in ShipCategoriesList.OrderBy(x => x.Category))
            {
                IEnumerable<ShipServiceView> services = ServicesList.Where(x => x.ShipSubType.ShipType.ShipCategory.Category == item.Category);

                if (services.Count() > 0)
                {
                    FleetItem fleet = new FleetItem(item.Category, services);
                    fleet.Id = item.Id;
                    fleet.Description = item.Category;
                    result.Add(fleet);
                }
            }

            return result;
        }

        private IEnumerable<FleetItem> GetFleetServicesByShipType()
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in ShipTypesList.OrderBy(x => x.Type))
            {
                IEnumerable<ShipServiceView> services = ServicesList.Where(x => x.ShipSubType.ShipType.Type == item.Type);

                if(services.Count() > 0)
                {
                    FleetItem fleet = new FleetItem(item.Type, services);
                    fleet.Id = item.Id;
                    fleet.Description = item.Type;
                    result.Add(fleet);
                }
            }

            return result;
        }

        private IEnumerable<FleetItem> GetFleetServicesByShipSubType()
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in ShipSubTypesList.OrderBy(x => x.Type))
            {
                IEnumerable<ShipServiceView> services = ServicesList.Where(x => x.ShipSubType.Type == item.Type);

                if (services.Count() > 0)
                {
                    FleetItem fleet = new FleetItem(item.Type, services);
                    fleet.Id = item.Id;
                    fleet.Description = item.Type;
                    result.Add(fleet);
                }
            }

            return result;
        }

        private IEnumerable<FleetItem> GetFleetServicesByShipClass()
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in ShipClassesList.OrderBy(x => x.ListName))
            {
                IEnumerable<ShipServiceView> services = ServicesList.Where(x => x.ShipClassId == item.Id);

                if (services.Count() > 0)
                {
                    FleetItem fleet = new FleetItem(item.ListName, services);
                    fleet.Id = item.Id;
                    fleet.Description = item.ListName;
                    result.Add(fleet);
                }
            }

            return result;
        }

        private IEnumerable<FleetItem> GetFleetServicesByBranch()
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in BranchesList)
            {
                IEnumerable<ShipServiceView> services = ServicesList.Where(x => x.Branch.Name == item.Name);

                if (services.Count() > 0)
                {
                    FleetItem fleet = new FleetItem(item.Name, services);
                    fleet.Id = item.Id;
                    fleet.Description = item.Name;
                    result.Add(fleet);
                }
            }

            return result;
        }

        #endregion Methods

    }
}
