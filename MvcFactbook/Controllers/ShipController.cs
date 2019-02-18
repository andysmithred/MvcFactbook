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
    public class ShipController : FactbookController<Ship, ShipView>
    {
        #region Private Declarations

        private DataAccess<Builder, BuilderView> builders = null;
        private ICollection<BuilderView> buildersList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<Builder, BuilderView> Builders
        {
            get => builders ?? (builders = new DataAccess<Builder, BuilderView>(Context, Context.Builder));
            set => builders = value;
        }

        public ICollection<BuilderView> BuildersList
        {
            get => buildersList ?? (buildersList = Builders.GetViews().OrderBy(x => x.ListName).ToList());
            set => buildersList = value;
        }

        #endregion Public Properties

        #region Constructor

        public ShipController(FactbookContext context)
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

        public async Task<IActionResult> ShipServicesList(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Launched,BuilderId")] Ship item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
            return View(item);
        }

        public IActionResult CreateByBuilder(int id)
        {
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByBuilder([Bind("Id,Name,Launched,BuilderId")] Ship item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Builder", new { id = item.BuilderId });
            }
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
            ViewBag.RouteId = item.BuilderId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, Item.BuilderId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,Launched,BuilderId")] Ship item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
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

        protected override DataAccess<Ship, ShipView> LoadDataAccess()
        {
            return new DataAccess<Ship, ShipView>(Context, Context.Ship);
        }

        protected override Func<int, Ship> GetItemFunction()
        {
            return i => Context.Ship
                        .Include(x => x.Builder)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<Ship>> GetItemsFunction()
        {
            return () => Context.Ship
                            .Include(x => x.Builder)
                            .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag);
        }

        protected override Func<Ship, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}