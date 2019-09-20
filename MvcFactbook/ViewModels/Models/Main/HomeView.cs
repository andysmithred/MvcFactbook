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
        private int armedForces = default(int);

        private ShipDataAccess shipsDb = null;
        private ShipServiceDataAccess shipServicesDb = null;
        private ShipClassDataAccess shipClassesDb = null;
        //private BuilderDataAccess buildersDb = null;
        private PoliticalEntityDataAccess politicalEntitiesDb = null;
        private FlagDataAccess flagDb = null;
        private BranchDataAccess branchDb = null;

        private ShipView featuredShip = null;
        private ShipServiceView featuredShipService = null;
        private ShipClassView featuredShipClass = null;
        private BuilderView featuredBuilder = null;
        private PoliticalEntityView featuredPoliticalEntity = null;
        private FlagView featuredFlag = null;
        private BranchView featuredBranch = null;

        private ICollection<ShipView> shipsList = null;
        private ICollection<ShipServiceView> startServiceList = null;
        private ICollection<ShipServiceView> endServiceList = null;
        private ICollection<PoliticalEntityView> startPoliticalEntitiesList = null;
        private ICollection<PoliticalEntityView> endPoliticalEntitiesList = null;

        #endregion Private Declarations

        #region Constructor

        public HomeView(FactbookContext context)
        {
            Context = context;
        }

        #endregion Constructor

        #region Public Properties

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The Factbook Context object has not been set.");
            set => context = value;
        }

        #region Counts

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

        //public CompleteItem<Builder> Builders
        //{
        //    get => builders ?? (builders = new CompleteItem<Builder>(Context.Builder));
        //    set => builders = value;
        //}

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

        public int ArmedForces
        {
            get => armedForces == default(int) ? armedForces = Context.ArmedForce.Count() : armedForces;
            set => armedForces = value;
        }

        #endregion Counts

        #region DataAccess

        public ShipDataAccess ShipsDb
        {
            get => shipsDb ?? (shipsDb = new ShipDataAccess(Context));
            set => shipsDb = value;
        }

        public ShipServiceDataAccess ShipServicesDb
        {
            get => shipServicesDb ?? (shipServicesDb = new ShipServiceDataAccess(Context));
            set => shipServicesDb = value;
        }

        public ShipClassDataAccess ShipClassesDb
        {
            get => shipClassesDb ?? (shipClassesDb = new ShipClassDataAccess(Context));
            set => shipClassesDb = value;
        }

        //public BuilderDataAccess BuildersDb
        //{
        //    get => buildersDb ?? (buildersDb = new BuilderDataAccess(Context));
        //    set => buildersDb = value;
        //}

        public PoliticalEntityDataAccess PoliticalEntitiesDb
        {
            get => politicalEntitiesDb ?? (politicalEntitiesDb = new PoliticalEntityDataAccess(Context));
            set => politicalEntitiesDb = value;
        }

        public FlagDataAccess FlagDb
        {
            get => flagDb ?? (flagDb = new FlagDataAccess(Context));
            set => flagDb = value;
        }

        public BranchDataAccess BranchDb
        {
            get => branchDb ?? (branchDb = new BranchDataAccess(Context));
            set => branchDb = value;
        }

        #endregion DataAccess

        #region Featured Items

        public ShipView FeaturedShip
        {
            get => featuredShip ?? (featuredShip = ShipsDb.GetRandomView());
            set => featuredShip = value;
        }

        public ShipServiceView FeaturedShipService
        {
            get => featuredShipService ?? (featuredShipService = ShipServicesDb.GetRandomView());
            set => featuredShipService = value;
        }

        public ShipClassView FeaturedShipClass
        {
            get => featuredShipClass ?? (featuredShipClass = ShipClassesDb.GetRandomView());
            set => featuredShipClass = value;
        }

        //public BuilderView FeaturedBuilder
        //{
        //    get => featuredBuilder ?? (featuredBuilder = BuildersDb.GetRandomView());
        //    set => featuredBuilder = value;
        //}

        public PoliticalEntityView FeaturedPoliticalEntity
        {
            get => featuredPoliticalEntity ?? (featuredPoliticalEntity = PoliticalEntitiesDb.GetRandomView());
            set => featuredPoliticalEntity = value;
        }

        public FlagView FeaturedFlag
        {
            get => featuredFlag ?? (featuredFlag = FlagDb.GetRandomView());
            set => featuredFlag = value;
        }

        public BranchView FeaturedBranch
        {
            get => featuredBranch ?? (featuredBranch = BranchDb.GetRandomView());
            set => featuredBranch = value;
        }

        #endregion Featured Items

        #region On this day

        public ICollection<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = ShipsDb.GetViews(GetShipsFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => shipsList = value;
        }

        public ICollection<ShipServiceView> StartServicesList
        {
            get => startServiceList ?? (startServiceList = ShipServicesDb.GetViews(GetStartServicesFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => startServiceList = value;
        }

        public ICollection<ShipServiceView> EndServicesList
        {
            get => endServiceList ?? (endServiceList = ShipServicesDb.GetViews(GetEndServicesFunction(DateTime.Now)).OrderBy(x => x.ListName).ToList());
            set => endServiceList = value;
        }

        public ICollection<PoliticalEntityView> StartPoliticalEntitiesList
        {
            get => startPoliticalEntitiesList ?? (startPoliticalEntitiesList = PoliticalEntitiesDb.GetViews().Where(GetStartPoliticalEntitiesFunction(DateTime.Now)).ToList());
            set => startPoliticalEntitiesList = value;
        }

        public ICollection<PoliticalEntityView> EndPoliticalEntitiesList
        {
            get => endPoliticalEntitiesList ?? (endPoliticalEntitiesList = PoliticalEntitiesDb.GetViews().Where(GetEndPoliticalEntitiesFunction(DateTime.Now)).ToList());
            set => endPoliticalEntitiesList = value;
        }

        #endregion On this day

        #endregion Public Properties

        #region Methods

        #region On this Day Function

        private Func<IQueryable<Ship>> GetShipsFunction(DateTime date)
        {
            return () => Context
                        .Ship
                        .Include(x => x.Dockyard)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.Branch)
                        .Where(x => x.Launched.Value.Month == date.Month && x.Launched.Value.Day == date.Day);
        }

        private Func<IQueryable<ShipService>> GetStartServicesFunction(DateTime date)
        {
            return () => Context
                        .ShipService
                        .Include(x => x.Branch)
                            .ThenInclude(x => x.BranchFlags)
                                .ThenInclude(x => x.Flag)
                        .Include(x => x.ShipSubType)
                        .Where(x => x.StartService.Value.Month == date.Month && x.StartService.Value.Day == date.Day);
        }

        private Func<IQueryable<ShipService>> GetEndServicesFunction(DateTime date)
        {
            return () => Context
                        .ShipService
                        .Include(x => x.Branch)
                            .ThenInclude(x => x.BranchFlags)
                                .ThenInclude(x => x.Flag)
                        .Include(x => x.ShipSubType)
                        .Where(x => x.EndService.Value.Month == date.Month && x.EndService.Value.Day == date.Day);
        }

        private Func<PoliticalEntityView, bool> GetStartPoliticalEntitiesFunction(DateTime date)
        {
            return x =>
            {
                if (x.StartDate.HasValue)
                {
                    if (x.StartDate.Value.Day == date.Day && x.StartDate.Value.Month == date.Month)
                        return true;
                }

                return false;
            };
        }

        private Func<PoliticalEntityView, bool> GetEndPoliticalEntitiesFunction(DateTime date)
        {
            return x =>
            {
                if (x.EndDate.HasValue)
                {
                    if (x.EndDate.Value.Day == date.Day && x.EndDate.Value.Month == date.Month)
                        return true;
                }

                return false;
            };
        }

        #endregion On this Day Function

        #endregion Methods

    }
}
