using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class FlagDataAccess
    {
        private DataAccess<Flag, FlagView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Flag, FlagView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Flag, FlagView>(Context, Context.Flag));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public FlagDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Flag, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Flag, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Flag> GetItems(Func<IQueryable<Flag>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Flag GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Flag GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<FlagView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<FlagView> GetViews(Func<IQueryable<Flag>> function)
        {
            return DataAccess.GetViews(function);
        }

        public FlagView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public FlagView GetView(int id, Func<int, Flag> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public FlagView GetRandomView()
        {
            FlagView view = new FlagView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<Flag> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<FlagView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<FlagView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Flag> GetItemFunction()
        {
            return i => Context.Flag
                        .Include(x => x.ArmedForceFlags).ThenInclude(x => x.ArmedForce)
                        .Include(x => x.BranchFlags).ThenInclude(x => x.Branch)
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.PoliticalEntity)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Flag>> GetItemsFunction()
        {
            return () => Context.Flag
                            .Include(x => x.ArmedForceFlags).ThenInclude(x => x.ArmedForce);
        }
    }
}
