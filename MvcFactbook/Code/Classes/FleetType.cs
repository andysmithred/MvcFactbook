using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Classes
{
    public class FleetType
    {
        #region Private Variables

        private FactbookContext context = null;

        private IEnumerable<ShipServiceView> shipServicesList = null;
        private IEnumerable<ShipView> shipsList = null;

        private IEnumerable<ShipCategoryView> shipCategoriesList = null;
        private IEnumerable<ShipTypeView> shipTypesList = null;
        private IEnumerable<ShipSubTypeView> shipSubTypesList = null;
        private IEnumerable<ShipClassView> shipClassesList = null;
        private IEnumerable<BranchView> branchesList = null;

        private IEnumerable<BuilderView> buildersList = null;

        #endregion Private Variables

        #region Constructor

        public FleetType(IEnumerable<ShipServiceView> services)
        {
            ShipServicesList = services;
        }

        public FleetType(IEnumerable<ShipView> ships)
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

        //public IEnumerable<BuilderView> BuildersList
        //{
        //    get => buildersList ?? (buildersList = new DataAccess<Builder, BuilderView>(Context, Context.Builder).GetViews());
        //    set => buildersList = value;
        //}

        public IEnumerable<ShipView> ShipsList
        {
            get => shipsList ?? (ShipsList = ShipServicesList.Select(x => x.Ship).Distinct(x => x.Id));
            set => shipsList = value;
        }

        public IEnumerable<ShipServiceView> ShipServicesList
        {
            get => shipServicesList ?? (shipServicesList = ShipsList.SelectMany(x => x.ShipServices).Distinct(x => x.Id));
            set => shipServicesList = value;
        }

        public IEnumerable<FleetItem> FleetServicesByShipCategory => GetFleetServicesByShipCategory(ShipServicesList, ShipCategoriesList);

        public IEnumerable<FleetItem> FleetServicesByShipType => GetFleetServicesByShipType(ShipServicesList, ShipTypesList);

        public IEnumerable<FleetItem> FleetServicesByShipSubType => GetFleetServicesByShipSubType(ShipServicesList, ShipSubTypesList);

        public IEnumerable<FleetItem> FleetServicesByShipClass => GetFleetServicesByShipClass(ShipServicesList, ShipClassesList);

        public IEnumerable<FleetItem> FleetServicesByBranch => GetFleetServicesByBranch(ShipServicesList, BranchesList);

        public FleetItem FleetServicesTotal => new FleetItem("Total", ShipServicesList);

        #endregion Public Properties

        #region Methods

        private IEnumerable<FleetItem> GetFleetServicesByShipCategory(IEnumerable<ShipServiceView> shipServicesList, IEnumerable<ShipCategoryView> shipCategoriesList)
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in shipCategoriesList.OrderBy(x => x.Category))
            {
                IEnumerable<ShipServiceView> services = shipServicesList.Where(x => x.ShipSubType.ShipType.ShipCategory.Category == item.Category);

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

        private IEnumerable<FleetItem> GetFleetServicesByShipType(IEnumerable<ShipServiceView> shipServicesList, IEnumerable<ShipTypeView> shipTypesList)
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in shipTypesList.OrderBy(x => x.Type))
            {
                IEnumerable<ShipServiceView> services = shipServicesList.Where(x => x.ShipSubType.ShipType.Type == item.Type);

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

        private IEnumerable<FleetItem> GetFleetServicesByShipSubType(IEnumerable<ShipServiceView> shipServicesList, IEnumerable<ShipSubTypeView> shipSubTypesList)
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in shipSubTypesList.OrderBy(x => x.Type))
            {
                IEnumerable<ShipServiceView> services = shipServicesList.Where(x => x.ShipSubType.Type == item.Type);

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

        private IEnumerable<FleetItem> GetFleetServicesByShipClass(IEnumerable<ShipServiceView> shipServicesList, IEnumerable<ShipClassView> shipClassesList)
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in shipClassesList.OrderBy(x => x.ListName))
            {
                IEnumerable<ShipServiceView> services = shipServicesList.Where(x => x.ShipClassId == item.Id);

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

        private IEnumerable<FleetItem> GetFleetServicesByBranch(IEnumerable<ShipServiceView> shipServicesList, IEnumerable<BranchView> branchesList)
        {
            List<FleetItem> result = new List<FleetItem>();

            foreach (var item in branchesList.OrderBy(x => x.Name))
            {
                IEnumerable<ShipServiceView> services = shipServicesList.Where(x => x.Branch.Name == item.Name);

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
