using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class BranchDataAccess
    {
        private DataAccess<Branch, BranchView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Branch, BranchView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Branch, BranchView>(Context, Context.Branch));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public BranchDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Branch, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Branch, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Branch> GetItems(Func<IQueryable<Branch>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Branch GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Branch GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<BranchView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<BranchView> GetViews(Func<IQueryable<Branch>> function)
        {
            return DataAccess.GetViews(function);
        }

        public BranchView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public BranchView GetView(int id, Func<int, Branch> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public BranchView GetRandomView()
        {
            BranchView view = new BranchView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<Branch> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<BranchView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<BranchView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Branch> GetItemFunction()
        {
            return i => Context.Branch
                        .Include(x => x.ArmedForce).ThenInclude(x => x.ArmedForceFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.BranchType)
                        .Include(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType).ThenInclude(x => x.ShipType).ThenInclude(x => x.ShipCategory)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Branch>> GetItemsFunction()
        {
            return () => Context.Branch
                                .Include(x => x.ArmedForce)
                                .Include(x => x.BranchType)
                                .Include(x => x.BranchFlags).ThenInclude(x => x.Flag);
        }

    }
}
