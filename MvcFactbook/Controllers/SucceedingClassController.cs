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
    public class SucceedingClassController : FactbookController<SucceedingClass, SucceedingClassView>
    {
        #region Private Declarations

        private DataAccess<ShipClass, ShipClassView> shipClasses = null;
        private ICollection<ShipClassView> shipClassesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ShipClass, ShipClassView> ShipClasses
        {
            get => shipClasses ?? (shipClasses = new DataAccess<ShipClass, ShipClassView>(Context, Context.ShipClass));
            set => shipClasses = value;
        }

        public ICollection<ShipClassView> ShipClassesList
        {
            get => shipClassesList ?? (shipClassesList = ShipClasses.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipClassesList = value;
        }

        #endregion Public Properties

        #region Constructor

        public SucceedingClassController(FactbookContext context)
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
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipClassId,SucceedingClassId")] SucceedingClass item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.SucceedingClassId);
            return View(item);
        }

        public IActionResult CreatePrecedingShipClass(int id)
        {
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePrecedingShipClass([Bind("ShipClassId,SucceedingClassId")] SucceedingClass item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipClass", new { id = item.SucceedingClassId });
            }
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.SucceedingClassId);
            ViewBag.RouteId = item.SucceedingClassId;
            return View(item);
        }

        public IActionResult CreateSucceedingShipClass(int id)
        {
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, id);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSucceedingShipClass([Bind("ShipClassId,SucceedingClassId")] SucceedingClass item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipClass", new { id = item.ShipClassId });
            }
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.SucceedingClassId);
            ViewBag.RouteId = item.ShipClassId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, Item.ShipClassId);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, Item.SucceedingClassId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,ShipClassId,SucceedingClassId")] SucceedingClass item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PrecedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.SucceedingClasses = GetSelectList<ShipClassView>(ShipClassesList, item.SucceedingClassId);
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

        protected override DataAccess<SucceedingClass, SucceedingClassView> LoadDataAccess()
        {
            return new DataAccess<SucceedingClass, SucceedingClassView>(Context, Context.SucceedingClass);
        }

        protected override Func<int, SucceedingClass> GetItemFunction()
        {
            return i => Context.SucceedingClass
                        .Include(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<SucceedingClass>> GetItemsFunction()
        {
            return () => Context.SucceedingClass
                                .Include(x => x.PrecedingShipClass)
                                .Include(x => x.SucceedingShipClass);
        }

        protected override Func<SucceedingClass, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}