using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;

namespace MvcFactbook.Controllers
{
    public class ShipTypeController : FactbookController<ShipType, ShipTypeView>
    {
        #region Private Declarations

        private DataAccess<ShipCategory, ShipCategoryView> shipCategories = null;
        private ICollection<ShipCategoryView> shipCategoriesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ShipCategory, ShipCategoryView> ShipCategories
        {
            get => shipCategories ?? (shipCategories = new DataAccess<ShipCategory, ShipCategoryView>(Context, Context.ShipCategory));
            set => shipCategories = value;
        }

        public ICollection<ShipCategoryView> ShipCategoriesList
        {
            get => shipCategoriesList ?? (shipCategoriesList = ShipCategories.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipCategoriesList = value;
        }

        #endregion Public Properties

        #region Constructor

        public ShipTypeController(FactbookContext context)
            : base(context) { }

        #endregion Constructor

        #region Index

        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }

        #endregion Index

        #region Details

        public override async Task<IActionResult> Details(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,ShipCategoryId")] ShipType item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, item.ShipCategoryId);
            return View(item);
        }

        public IActionResult CreateByShipCategory(int id)
        {
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipCategory([Bind("Id,Type,ShipCategoryId")] ShipType item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipCategory", new { id = item.ShipCategoryId });
            }
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, item.ShipCategoryId);
            ViewBag.RouteId = item.ShipCategoryId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, Item.ShipCategoryId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Type,ShipCategoryId")] ShipType item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.ShipCategories = GetSelectList<ShipCategoryView>(ShipCategoriesList, item.ShipCategoryId);
            return result;
        }

        #endregion Edit

        #region Delete

        public override async Task<IActionResult> Delete(int? id)
        {
            return await base.Delete(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> DeleteConfirmed(int id)
        {
            return await base.DeleteConfirmed(id);
        }

        #endregion Delete

        #region Override Abstract Methods

        protected override DataAccess<ShipType, ShipTypeView> LoadDataAccess()
        {
            return new DataAccess<ShipType, ShipTypeView>(Context, Context.ShipType);
        }

        protected override Func<int, ShipType> GetItemFunction()
        {
            return i => Context.ShipType
                        .Include(x => x.ShipCategory)
                        .Include(x => x.ShipSubTypes)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipType>> GetItemsFunction()
        {
            return () => Context.ShipType
                                .Include(x => x.ShipCategory);
        }

        protected override Func<ShipType, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}