using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Classes;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class HomeView
    {
        #region Private Declarations

        private FactbookContext context = null;
        private CompleteItem<ShipClass> shipClasses = null;
        private CompleteItem<Ship> ships = null;
        private CompleteItem<ShipService> shipServices = null;
        private CompleteItem<Builder> builders = null;
        private CompleteItem<Branch> branches = null;
        private CompleteItem<Flag> flags = null;
        private CompleteItem<PoliticalEntity> politicalEntities = null;






        #endregion Private Declarations

        #region Public Properties

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The Factbook Context object has not been set.");
            set => context = value;
        }

        public CompleteItem<ShipClass> ShipClasses
        {
            get => shipClasses ?? (shipClasses = new CompleteItem<ShipClass>(Context.ShipClass));
            set => shipClasses = value;
        }

        public CompleteItem<Ship> Ships
        {
            get => ships ?? (ships = new CompleteItem<Ship>(Context.Ship));
            set => ships = value;
        }

        public CompleteItem<ShipService> ShipServices
        {
            get => shipServices ?? (shipServices = new CompleteItem<ShipService>(Context.ShipService));
            set => shipServices = value;
        }

        public CompleteItem<Builder> Builders
        {
            get => builders ?? (builders = new CompleteItem<Builder>(Context.Builder));
            set => builders = value;
        }

        public CompleteItem<Branch> Branches
        {
            get => branches ?? (branches = new CompleteItem<Branch>(Context.Branch));
            set => branches = value;
        }

        public CompleteItem<Flag> Flags
        {
            get => flags ?? (flags = new CompleteItem<Flag>(Context.Flag));
            set => flags = value;
        }

        public CompleteItem<PoliticalEntity> PoliticalEntities
        {
            get => politicalEntities ?? (politicalEntities = new CompleteItem<PoliticalEntity>(Context.PoliticalEntity));
            set => politicalEntities = value;
        }



        private IEnumerable<Flag> x = null;
        public IEnumerable<Flag> X
        {
            get => x ?? (x = GetItemsFunction(new DateTime(2008, 1, 17)));
            set => x = value;
        }

        protected IEnumerable<Flag> GetItemsFunction(DateTime date)
        {
            return Context
                        .Flag
                        .Include(x => x.BranchFlags)
                            .ThenInclude(x => x.Branch)
                        .Include(x => x.ArmedForceFlags)
                            .ThenInclude(x => x.ArmedForce)
                        .Where(x => x.StartDate.Value.Month == date.Month && x.StartDate.Value.Day == date.Day);
        }


        private DataAccess<Ship, ShipView> dbShips = null;
        private ICollection<ShipView> shipsList = null;

        public DataAccess<Ship, ShipView> DbShips
        {
            get => dbShips ?? (dbShips = new DataAccess<Ship, ShipView>(Context, Context.Ship));
            set => dbShips = value;
        }

        public ICollection<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = DbShips.GetViews(GetShipsFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => shipsList = value;
        }

        protected Func<IQueryable<Ship>> GetShipsFunction(DateTime date)
        {
            return () => Context
                        .Ship
                        .Include(x => x.Builder)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.Branch)
                        .Where(x => x.Launched.Value.Month == date.Month && x.Launched.Value.Day == date.Day);
        }

        private DataAccess<ShipService, ShipServiceView> dbShipServices = null;

        private ICollection<ShipServiceView> startServiceList = null;
        private ICollection<ShipServiceView> endServiceList = null;


        public DataAccess<ShipService, ShipServiceView> DbShipServices
        {
            get => dbShipServices ?? (dbShipServices = new DataAccess<ShipService, ShipServiceView>(Context, Context.ShipService));
            set => dbShipServices = value;
        }

        public ICollection<ShipServiceView> StartServicesList
        {
            get => startServiceList ?? (startServiceList = DbShipServices.GetViews(GetStartServicesFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => startServiceList = value;
        }

        public ICollection<ShipServiceView> EndServicesList
        {
            get => endServiceList ?? (endServiceList = DbShipServices.GetViews(GetEndServicesFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => endServiceList = value;
        }

        protected Func<IQueryable<ShipService>> GetStartServicesFunction(DateTime date)
        {
            return () => Context
                        .ShipService
                        .Where(x => x.StartService.Value.Month == date.Month && x.StartService.Value.Day == date.Day);
        }

        protected Func<IQueryable<ShipService>> GetEndServicesFunction(DateTime date)
        {
            return () => Context
                        .ShipService
                        .Where(x => x.EndService.Value.Month == date.Month && x.EndService.Value.Day == date.Day);
        }

        private int armedForces = default(int);

        public int ArmedForces
        {
            get => armedForces == default(int) ? armedForces = Context.ArmedForce.Count() : armedForces;
            set => armedForces = value;
        }

        #endregion Public Properties

        #region Constructor

        public HomeView(FactbookContext context)
        {
            Context = context;
        }

        #endregion Constructor

    }
}
