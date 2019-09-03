using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class BuilderDataAccess
    {
        private DataAccess<Builder, BuilderView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Builder, BuilderView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Builder, BuilderView>(Context, Context.Builder));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public BuilderDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Builder, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Builder, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Builder> GetItems(Func<IQueryable<Builder>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Builder GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Builder GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<BuilderView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<BuilderView> GetViews(Func<IQueryable<Builder>> function)
        {
            return DataAccess.GetViews(function);
        }

        public BuilderView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public BuilderView GetView(int id, Func<int, Builder> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public BuilderView GetRandomView()
        {
            BuilderView view = new BuilderView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<Builder> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<BuilderView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<BuilderView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Builder> GetItemFunction()
        {
            return i => Context
                        .Builder
                        .Include(x => x.Ships)
                        .Include(x => x.PoliticalEntityBuilders)
                            .ThenInclude(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityFlags)
                                    .ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityBuilders)
                            .ThenInclude(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityEras)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Builder>> GetItemsFunction()
        {
            return () => Context
                        .Builder
                        .Include(x => x.Ships)
                        .Include(x => x.PoliticalEntityBuilders).ThenInclude(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag);
        }

    }
}
