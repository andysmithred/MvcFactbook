using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class ShipClassDataAccess
    {
        private DataAccess<ShipClass, ShipClassView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<ShipClass, ShipClassView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<ShipClass, ShipClassView>(Context, Context.ShipClass));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public ShipClassDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<ShipClass, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<ShipClass, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<ShipClass> GetItems(Func<IQueryable<ShipClass>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public ShipClass GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public ShipClass GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<ShipClassView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<ShipClassView> GetViews(Func<IQueryable<ShipClass>> function)
        {
            return DataAccess.GetViews(function);
        }

        public ShipClassView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public ShipClassView GetView(int id, Func<int, ShipClass> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public ShipClassView GetRandomView()
        {
            ShipClassView view = new ShipClassView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<ShipClass> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<ShipClassView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<ShipClassView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }


        private Func<int, ShipClass> GetItemFunction()
        {
            return i => Context.ShipClass
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship).ThenInclude(x => x.Builder)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<ShipClass>> GetItemsFunction()
        {
            return () => Context.ShipClass
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship);
        }
    }
}
