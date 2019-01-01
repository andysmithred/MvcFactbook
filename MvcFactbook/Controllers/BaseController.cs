using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public abstract class BaseController<T, TView> : Controller
        where T : class
        where TView : IView<T>, new()
    {
        #region Private Declarations

        private DataAccess<T, TView> dataAccess = null;

        #endregion Private Declarations

        #region Properties

        public DataAccess<T, TView> DataAccess
        {
            get => dataAccess ?? (dataAccess = LoadDataAccess());
            set => dataAccess = value;
        }

        public TView ItemView { get; set; } = default(TView);

        public T Item { get; set; } = default(T);

        #endregion Properties

        #region Methods

        #region List

        public virtual async Task<IActionResult> Index()
        {
            return View(await DataAccess.GetViewsAsync(GetItemsFunction()));
        }

        #endregion List

        #region Details

        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await DataAccess.GetViewAsync(id.Value, GetItemFunction());

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        #endregion Details

        #region Create

        public virtual IActionResult Create()
        {
            return View();
        }

        public void Add(T item)
        {
            DataAccess.Add(item);
            DataAccess.Save();
        }

        public Task<int> AddAsync(T item)
        {
            DataAccess.Add(item);
            return DataAccess.SaveAsync();
        }

        #endregion Create

        #region Edit

        public virtual async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await DataAccess.GetItemAsync(id.Value, GetItemFunction());

            if (Item == null)
            {
                return NotFound();
            }

            return View(Item);
        }

        public virtual async Task<IActionResult> Edit(int itemId, T item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateAsync(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataAccess.ItemExists(GetExistsFunc(itemId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = itemId });
            }
            return View(item);
        }

        public void Update(T item)
        {
            DataAccess.Update(item);
            DataAccess.Save();
        }

        public async Task<int> UpdateAsync(T item)
        {
            DataAccess.Update(item);
            return await DataAccess.SaveAsync();
        }

        #endregion Edit

        #region Delete

        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await DataAccess.GetItemAsync(id.Value, GetItemFunction());

            if (Item == null)
            {
                return NotFound();
            }

            return View(Item);
        }

        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            Item = await DataAccess.GetItemAsync(id, GetItemFunction());
            DataAccess.Delete(Item);
            await DataAccess.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion Delete

        #region Abstract Methods

        protected abstract DataAccess<T, TView> LoadDataAccess();
        protected abstract Func<int, T> GetItemFunction();
        protected abstract Func<IQueryable<T>> GetItemsFunction();
        protected abstract Func<T, bool> GetExistsFunc(int id);

        #endregion Abstract Methods

        #region Other Methods

        protected SelectList GetSelectList<TListType>(IEnumerable<TListType> list, int? id)
        {
            return id.HasValue ? new SelectList(list, "Id", "ListName", id) : new SelectList(list, "Id", "ListName");
        }

        #endregion Other Methods

        #endregion Methods
    }
}