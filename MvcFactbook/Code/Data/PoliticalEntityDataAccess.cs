using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class PoliticalEntityDataAccess
    {
        private DataAccess<PoliticalEntity, PoliticalEntityView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<PoliticalEntity, PoliticalEntityView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<PoliticalEntity, PoliticalEntityView>(Context, Context.PoliticalEntity));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public PoliticalEntityDataAccess(FactbookContext context)
        {
            Context = context;
        }


        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<PoliticalEntity, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<PoliticalEntity, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<PoliticalEntity> GetItems(Func<IQueryable<PoliticalEntity>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public PoliticalEntity GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public PoliticalEntity GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<PoliticalEntityView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<PoliticalEntityView> GetViews(Func<IQueryable<PoliticalEntity>> function)
        {
            return DataAccess.GetViews(function);
        }

        public PoliticalEntityView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public PoliticalEntityView GetView(int id, Func<int, PoliticalEntity> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public PoliticalEntityView GetRandomView()
        {
            PoliticalEntityView view = new PoliticalEntityView();
            view.ViewObject = GetRandomItem();
            return view;
        }









        public Task<PoliticalEntity> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }


        public Task<ICollection<PoliticalEntityView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());

        }

        public Task<PoliticalEntityView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, PoliticalEntity> GetItemFunction()
        {
            return i => Context
                        .PoliticalEntity
                        .Include(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PrecedingEntities).ThenInclude(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PrecedingEntities).ThenInclude(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.SucceedingEntities).ThenInclude(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.SucceedingEntities).ThenInclude(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityBuilders).ThenInclude(x => x.Builder).ThenInclude(x => x.Ships)
                        .Include(x => x.PoliticalEntityEras)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<PoliticalEntity>> GetItemsFunction()
        {
            return () => Context
                        .PoliticalEntity
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntityEras);
        }

    }
}
