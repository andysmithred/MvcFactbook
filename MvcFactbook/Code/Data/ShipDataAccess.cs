using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class ShipDataAccess
    {
        private DataAccess<Ship, ShipView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Ship, ShipView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Ship, ShipView>(Context, Context.Ship));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public ShipDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Ship, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Ship, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Ship> GetItems(Func<IQueryable<Ship>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Ship GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Ship GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<ShipView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<ShipView> GetViews(Func<IQueryable<Ship>> function)
        {
            return DataAccess.GetViews(function);
        }

        public ShipView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public ShipView GetView(int id, Func<int, Ship> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public ShipView GetRandomView()
        {
            ShipView view = new ShipView();
            view.ViewObject = GetRandomItem();
            return view;
        }

       







        public Task<Ship> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<ShipView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());
            
        }

        public Task<ShipView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Ship> GetItemFunction()
        {
            return i => Context.Ship
                        .Include(x => x.Dockyard)
                            //.ThenInclude(x => x.PoliticalEntityBuilders)
                            //    .ThenInclude(x => x.PoliticalEntity)
                            //        .ThenInclude(x => x.PoliticalEntityFlags)
                            //            .ThenInclude(x => x.Flag)
                        //.Include(x => x.Builder)
                            //.ThenInclude(x => x.PoliticalEntityBuilders)
                                //.ThenInclude(x => x.PoliticalEntity)
                                //    .ThenInclude(x => x.PoliticalEntityEras)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.Branch)
                                .ThenInclude(x => x.BranchFlags)
                                    .ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.ShipClass)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Ship>> GetItemsFunction()
        {
            return () => Context.Ship
                            .Include(x => x.Dockyard)
                            .Include(x => x.ShipServices)
                                .ThenInclude(x => x.Branch)
                                    .ThenInclude(x => x.BranchFlags)
                                        .ThenInclude(x => x.Flag);
        }




    }
}
