using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class ShipbuilderDataAccess
    {
        private DataAccess<Shipbuilder, ShipbuilderView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Shipbuilder, ShipbuilderView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Shipbuilder, ShipbuilderView>(Context, Context.Shipbuilder));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public ShipbuilderDataAccess(FactbookContext context)
        {
            Context = context;
        }

        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Shipbuilder, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Shipbuilder, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Shipbuilder> GetItems(Func<IQueryable<Shipbuilder>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Shipbuilder GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Shipbuilder GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<ShipbuilderView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<ShipbuilderView> GetViews(Func<IQueryable<Shipbuilder>> function)
        {
            return DataAccess.GetViews(function);
        }

        public ShipbuilderView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public ShipbuilderView GetView(int id, Func<int, Shipbuilder> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public ShipbuilderView GetRandomView()
        {
            ShipbuilderView view = new ShipbuilderView();
            view.ViewObject = GetRandomItem();
            return view;
        }

        public Task<Shipbuilder> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }

        public Task<ICollection<ShipbuilderView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<ShipbuilderView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Shipbuilder> GetItemFunction()
        {
            return i => Context
                        .Shipbuilder
                        //.Include(x => x.Ships)
                        //.Include(x => x.PoliticalEntityBuilders)
                        //    .ThenInclude(x => x.PoliticalEntity)
                        //        .ThenInclude(x => x.PoliticalEntityFlags)
                        //            .ThenInclude(x => x.Flag)
                        //.Include(x => x.PoliticalEntityBuilders)
                        //    .ThenInclude(x => x.PoliticalEntity)
                        //        .ThenInclude(x => x.PoliticalEntityEras)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Shipbuilder>> GetItemsFunction()
        {
            return () => Context
                        .Shipbuilder;
                        //.Include(x => x.Ships)
                        //.Include(x => x.PoliticalEntityBuilders).ThenInclude(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag);
        }

    }
}
