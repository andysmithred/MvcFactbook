using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public class BranchController : FactbookController<Branch, BranchView>
    {
        #region Private Declarations

        private DataAccess<ArmedForce, ArmedForceView> armedForces = null;
        private ICollection<ArmedForceView> armedForcesList = null;

        private DataAccess<BranchType, BranchTypeView> branchTypes = null;
        private ICollection<BranchTypeView> branchTypesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ArmedForce, ArmedForceView> ArmedForces
        {
            get => armedForces ?? (armedForces = new DataAccess<ArmedForce, ArmedForceView>(Context, Context.ArmedForce));
            set => armedForces = value;
        }

        public ICollection<ArmedForceView> ArmedForcesList
        {
            get => armedForcesList ?? (armedForcesList = ArmedForces.GetViews().OrderBy(x => x.ListName).ToList());
            set => armedForcesList = value;
        }

        public DataAccess<BranchType, BranchTypeView> BranchTypes
        {
            get => branchTypes ?? (branchTypes = new DataAccess<BranchType, BranchTypeView>(Context, Context.BranchType));
            set => branchTypes = value;
        }

        public ICollection<BranchTypeView> BranchTypesList
        {
            get => branchTypesList ?? (branchTypesList = BranchTypes.GetViews().OrderBy(x => x.ListName).ToList());
            set => branchTypesList = value;
        }

        #endregion Public Properties

        #region Constructor

        public BranchController(FactbookContext context)
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

        public async Task<IActionResult> FlagsList(int? id)
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
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, null);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArmedForceId,BranchTypeId,HasEmblem")] Branch item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, item.BranchTypeId);
            return View(item);
        }

        public IActionResult CreateByArmedForce(int id)
        {
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, id);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByArmedForce([Bind("Id,Name,ArmedForceId,BranchTypeId,HasEmblem")] Branch item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ArmedForce", new { id = item.ArmedForceId });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, item.BranchTypeId);
            ViewBag.RouteId = item.ArmedForceId;
            return View(item);
        }

        public IActionResult CreateByBranchType(int id)
        {
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, null);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByBranchType([Bind("Id,Name,ArmedForceId,BranchTypeId,HasEmblem")] Branch item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "BranchType", new { id = item.BranchTypeId });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, item.BranchTypeId);
            ViewBag.RouteId = item.BranchTypeId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, Item.ArmedForceId);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, Item.BranchTypeId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,ArmedForceId,BranchTypeId,HasEmblem")] Branch item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.BranchTypes = GetSelectList<BranchTypeView>(BranchTypesList, item.BranchTypeId);
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

        protected override DataAccess<Branch, BranchView> LoadDataAccess()
        {
            return new DataAccess<Branch, BranchView>(Context, Context.Branch);
        }

        protected override Func<int, Branch> GetItemFunction()
        {
            return i => Context.Branch
                        .Include(x => x.ArmedForce).ThenInclude(x => x.ArmedForceFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.BranchType)
                        .Include(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipClass)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<Branch>> GetItemsFunction()
        {
            return () => Context.Branch
                                .Include(x => x.ArmedForce)
                                .Include(x => x.BranchType)
                                .Include(x => x.BranchFlags).ThenInclude(x => x.Flag);
        }

        protected override Func<Branch, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}