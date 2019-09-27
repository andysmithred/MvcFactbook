using Microsoft.EntityFrameworkCore;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class DockyardDataAccess
    {
        private DataAccess<Dockyard, DockyardView> dataAccess = null;
        private FactbookContext context = null;

        public DataAccess<Dockyard, DockyardView> DataAccess
        {
            get => dataAccess ?? (dataAccess = new DataAccess<Dockyard, DockyardView>(Context, Context.Dockyard));
            set => dataAccess = value;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public DockyardDataAccess(FactbookContext context)
        {
            Context = context;
        }

        public int Count()
        {
            return DataAccess.Count();
        }

        public int Count(Func<Dockyard, bool> predicate)
        {
            return DataAccess.Count(predicate);
        }

        public bool ItemExists(Func<Dockyard, bool> predicate)
        {
            return DataAccess.ItemExists(predicate);
        }

        public virtual IQueryable<Dockyard> GetItems(Func<IQueryable<Dockyard>> itemFunc)
        {
            return DataAccess.GetItems(GetItemsFunction());
        }

        public Dockyard GetItem(int id)
        {
            return DataAccess.GetItem(id, GetItemFunction());
        }

        public Dockyard GetRandomItem()
        {
            //The GetRandonItem will return a skinny object without builders etc...
            return GetItem(DataAccess.GetRandomItem().Id);
        }

        //public T GetRandomItem(Func<T, bool> itemFunc)
        //{
        //    return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        //}

        public ICollection<DockyardView> GetViews()
        {
            return DataAccess.GetViews(GetItemsFunction());
        }

        public ICollection<DockyardView> GetViews(Func<IQueryable<Dockyard>> function)
        {
            return DataAccess.GetViews(function);
        }

        public DockyardView GetView(int id)
        {
            return DataAccess.GetView(id, GetItemFunction());
        }

        public DockyardView GetView(int id, Func<int, Dockyard> itemFunc)
        {
            return DataAccess.GetView(id, itemFunc);
        }

        public DockyardView GetRandomView()
        {
            DockyardView view = new DockyardView();
            view.ViewObject = GetRandomItem();
            return view;
        }

        public Task<Dockyard> GetItemAsync(int id)
        {
            return DataAccess.GetItemAsync(id, GetItemFunction());
        }

        public Task<ICollection<DockyardView>> GetViewsAsync()
        {
            return DataAccess.GetViewsAsync(GetItemsFunction());
        }

        public Task<DockyardView> GetViewAsync(int id)
        {
            return DataAccess.GetViewAsync(id, GetItemFunction());
        }

        private Func<int, Dockyard> GetItemFunction()
        {
            return i => Context
                        .Dockyard
                        .Include(x => x.Ships)
                        .Include(x => x.PoliticalEntityDockyards)
                            .ThenInclude(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityFlags)
                                    .ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityDockyards)
                            .ThenInclude(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityEras)
                        .FirstOrDefault(x => x.Id == i);
        }

        private Func<IQueryable<Dockyard>> GetItemsFunction()
        {
            return () => Context
                        .Dockyard
                        .Include(x => x.Ships)
                        .Include(x => x.PoliticalEntityDockyards)
                            .ThenInclude(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityFlags)
                                    .ThenInclude(x => x.Flag);
        }
    }
}
