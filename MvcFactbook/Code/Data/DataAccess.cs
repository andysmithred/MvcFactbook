using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class DataAccess<T, TView>
        where T : class
        where TView : IView<T>, new()
    {
        #region Private Declarations

        private DbSet<T> dataSet = null;
        private DbContext context = null;
        private Random random = null;

        #endregion Private Declarations

        #region Public Properties

        public DbSet<T> DataSet
        {
            get => dataSet ?? throw new NullReferenceException("The dataSet has not been set.");
            set => dataSet = value;
        }

        public DbContext Context
        {
            get => context ?? throw new NullReferenceException("The context object has not been set.");
            set => context = value;
        }

        public Random Random
        {
            get => random ?? (random = new Random());
        }

        #endregion Public Properties

        #region Constructor

        public DataAccess(DbContext context, DbSet<T> dataSet)
        {
            Context = context;
            DataSet = dataSet;
        }

        #endregion Constructor

        #region Methods

        #region Synchronous Methods

        public int Count()
        {
            return DataSet.Count();
        }

        public int Count(Func<T, bool> predicate)
        {
            return DataSet.Count(predicate);
        }

        public bool ItemExists(Func<T, bool> predicate)
        {
            return DataSet.Any(predicate);
        }




        public virtual IQueryable<T> GetItems()
        {
            return from item in DataSet
                   select item;
        }

        public virtual IQueryable<T> GetItems(Func<IQueryable<T>> itemFunc)
        {
            return itemFunc();
        }



        public T GetItem(int id)
        {
            return DataSet.Find(id);
        }

        public T GetItem(int id, Func<int, T> itemFunc)
        {
            return itemFunc(id);
        }




        public T GetRandomItem()
        {
            return GetItems().ToList().ElementAt(Random.Next(Count()));
        }

        public T GetRandomItem(Func<T, bool> itemFunc)
        {
            return DataSet.Where(itemFunc).ElementAt(Random.Next(Count(itemFunc)));
        }

     



        public void Add(T item)
        {
            DataSet.Add(item);
        }

        public void Delete(T item)
        {
            DataSet.Remove(item);
        }

        public void Update(T item)
        {
            Context.Entry<T>(item).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return Context.SaveChangesAsync();
        }

        public ICollection<TView> GetViews()
        {
            return GetViews(GetItems);
        }

        public ICollection<TView> GetViews(Func<IQueryable<T>> function)
        {
            ICollection<TView> result = new List<TView>();
            foreach (var item in function())
            {
                TView view = new TView();
                view.ViewObject = item;
                result.Add(view);
            }
            return result;
        }

        public TView GetView(int id)
        {
            return GetView(id, GetItem);
        }

        public TView GetView(int id, Func<int, T> itemFunc)
        {
            TView view = new TView();
            view.ViewObject = GetItem(id, itemFunc);
            return view;
        }

        public TView GetRandomView()
        {
            TView view = new TView();
            view.ViewObject = GetRandomItem();
            return view;
        }

        public TView GetRandomView(Func<T, bool> itemFunc)
        {
            TView view = new TView();
            view.ViewObject = GetRandomItem(itemFunc);
            return view;
        }

        #endregion Synchronous Methods

        #region Asynchronous Methods

        public Task<ICollection<TView>> GetViewsAsync()
        {
            return Task<ICollection<TView>>.Factory.StartNew(() => GetViews());
        }

        public Task<ICollection<TView>> GetViewsAsync(Func<IQueryable<T>> function)
        {
            return Task<ICollection<TView>>.Factory.StartNew(() => GetViews(function));
        }

        public Task<TView> GetViewAsync(int id, Func<int, T> function)
        {
            return Task<TView>.Factory.StartNew(() => GetView(id, function));
        }

        public Task<T> GetItemAsync(int id, Func<int, T> itemFunc)
        {
            return Task<T>.Factory.StartNew(() => GetItem(id, itemFunc));
        }

        #endregion Asynchronous Methods

        #endregion Methods

    }
}
