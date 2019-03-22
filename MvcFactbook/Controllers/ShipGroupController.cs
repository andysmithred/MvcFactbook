using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public class ShipGroupController : FactbookController<ShipGroup, ShipGroupView>
    {
        #region Constructor

        public ShipGroupController(FactbookContext context)
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
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Icon")] ShipGroup item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            return await base.Edit(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Icon")] ShipGroup item)
        {
            return await base.Edit(id, item);
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

        protected override DataAccess<ShipGroup, ShipGroupView> LoadDataAccess()
        {
            return new DataAccess<ShipGroup, ShipGroupView>(Context, Context.ShipGroup);
        }

        protected override Func<int, ShipGroup> GetItemFunction()
        {
            return i => Context
                        .ShipGroup
                        .Include(x => x.ShipGroupSets).ThenInclude(x => x.ShipService).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipGroupSets).ThenInclude(x => x.ShipService).ThenInclude(x => x.ShipClass)
                        .Include(x => x.ShipGroupSets).ThenInclude(x => x.ShipService).ThenInclude(x => x.ShipSubType)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipGroup>> GetItemsFunction()
        {
            return () => Context.ShipGroup;
        }

        protected override Func<ShipGroup, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}
