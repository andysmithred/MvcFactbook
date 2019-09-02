using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class ShipServiceDataAccess
    {
        private DataAccess<ShipService, ShipServiceView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<ShipService, ShipServiceView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<ShipService, ShipServiceView>(Context, Context.ShipService));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public ShipServiceDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<ShipService, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<ShipService, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<ShipService> GetItems(Func<IQueryable<ShipService>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public ShipService GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public ShipService GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<ShipServiceView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<ShipServiceView> GetViews(Func<IQueryable<ShipService>> function)
        {
            return DataAccess.GetViews(function);
        }

        public ShipServiceView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public ShipServiceView GetView(int id, Func<int, ShipService> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public ShipServiceView GetRandomView()
        {
            ShipServiceView view = new ShipServiceView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<ShipService> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<ShipServiceView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<ShipServiceView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }


        private Func<int, ShipService> GetItemFunction()
        {
            return i => Context.ShipService
                        .Include(x => x.Ship).ThenInclude(x => x.Builder)
                        .Include(x => x.ShipClass)
                        .Include(x => x.ShipSubType).ThenInclude(x => x.ShipType).ThenInclude(x => x.ShipCategory)
                        .Include(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.Branch).ThenInclude(x => x.ArmedForce)
                        .Include(x => x.ShipGroupSets).ThenInclude(x => x.ShipGroup)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<ShipService>> GetItemsFunction()
        {
            return () => Context.ShipService
                                .Include(x => x.Ship)
                                .Include(x => x.ShipClass)
                                .Include(x => x.ShipSubType)
                                .Include(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag);
        }
    }
}
