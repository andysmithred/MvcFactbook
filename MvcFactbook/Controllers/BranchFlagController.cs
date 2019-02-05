using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MvcFactbook.Controllers
{
    public class BranchFlagController : FactbookController<BranchFlag, BranchFlagView>
    {
        #region Private Declarations

        private DataAccess<Branch, BranchView> branches = null;
        private ICollection<BranchView> branchesList = null;

        private DataAccess<Flag, FlagView> flags = null;
        private ICollection<FlagView> flagsList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<Branch, BranchView> Branches
        {
            get => branches ?? (branches = new DataAccess<Branch, BranchView>(Context, Context.Branch));
            set => branches = value;
        }

        public ICollection<BranchView> BranchesList
        {
            get => branchesList ?? (branchesList = Branches.GetViews().OrderBy(x => x.ListName).ToList());
            set => branchesList = value;
        }

        public DataAccess<Flag, FlagView> Flags
        {
            get => flags ?? (flags = new DataAccess<Flag, FlagView>(Context, Context.Flag));
            set => flags = value;
        }

        public ICollection<FlagView> FlagsList
        {
            get => flagsList ?? (flagsList = Flags.GetViews().OrderBy(x => x.ListName).ToList());
            set => flagsList = value;
        }

        #endregion Public Properties

        #region Constructor

        public BranchFlagController(FactbookContext context)
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
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,FlagId,Start,End")] BranchFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            return View(item);
        }

        public IActionResult CreateByBranch(int id)
        {
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, id);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByBranch([Bind("Id,BranchId,FlagId,Start,End")] BranchFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Branch", new { id = item.BranchId });
            }
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.RouteId = item.BranchId;
            return View(item);
        }

        public IActionResult CreateByFlag(int id)
        {
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFlag([Bind("Id,BranchId,FlagId,Start,End")] BranchFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Flag", new { id = item.FlagId });
            }
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.RouteId = item.FlagId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, Item.BranchId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, Item.FlagId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,FlagId,Start,End")] BranchFlag item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
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

        protected override DataAccess<BranchFlag, BranchFlagView> LoadDataAccess()
        {
            return new DataAccess<BranchFlag, BranchFlagView>(Context, Context.BranchFlag);
        }

        protected override Func<int, BranchFlag> GetItemFunction()
        {
            return i => Context.BranchFlag
                        .Include(x => x.Branch).ThenInclude(x => x.ArmedForce)
                        .Include(x => x.Branch).ThenInclude(x => x.BranchType)
                        .Include(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<BranchFlag>> GetItemsFunction()
        {
            return () => Context.BranchFlag
                                .Include(x => x.Branch).ThenInclude(x => x.ArmedForce)
                                .Include(x => x.Branch).ThenInclude(x => x.BranchType)
                                .Include(x => x.Flag);
        }

        protected override Func<BranchFlag, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}