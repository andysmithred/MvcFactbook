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
    public class ShipServiceController : FactbookController<ShipService, ShipServiceView>
    {
        #region Private Declarations

        private DataAccess<Ship, ShipView> ships = null;
        private ICollection<ShipView> shipsList = null;

        private DataAccess<ShipClass, ShipClassView> shipClasses = null;
        private ICollection<ShipClassView> shipClassesList = null;

        private DataAccess<ShipSubType, ShipSubTypeView> shipSubTypes = null;
        private ICollection<ShipSubTypeView> shipSubTypesList = null;

        private DataAccess<Branch, BranchView> branches = null;
        private ICollection<BranchView> branchesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<Ship, ShipView> Ships
        {
            get => ships ?? (ships = new DataAccess<Ship, ShipView>(Context, Context.Ship));
            set => ships = value;
        }

        public ICollection<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = Ships.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipsList = value;
        }

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

        public DataAccess<ShipSubType, ShipSubTypeView> ShipSubTypes
        {
            get => shipSubTypes ?? (shipSubTypes = new DataAccess<ShipSubType, ShipSubTypeView>(Context, Context.ShipSubType));
            set => shipSubTypes = value;
        }

        public ICollection<ShipSubTypeView> ShipSubTypesList
        {
            get => shipSubTypesList ?? (shipSubTypesList = ShipSubTypes.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipSubTypesList = value;
        }

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

        #endregion Public Properties

        #region Constructor

        public ShipServiceController(FactbookContext context)
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
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, null);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, null);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            return View(item);
        }

        public IActionResult CreateByShip(int id)
        {
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, id);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, null);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShip([Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Ship", new { id = item.ShipId });
            }
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.RouteId = item.ShipId;
            return View(item);
        }

        public IActionResult CreateByShipClass(int id)
        {
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, null);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, id);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, null);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipClass([Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipClass", new { id = item.ShipClassId });
            }
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.RouteId = item.ShipClassId;
            return View(item);
        }

        public IActionResult CreateByShipSubType(int id)
        {
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, null);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, id);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipSubType([Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipSubType", new { id = item.ShipSubTypeId });
            }
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.RouteId = item.ShipSubTypeId;
            return View(item);
        }

        public IActionResult CreateByBranch(int id)
        {
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, null);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, null);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, null);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByBranch([Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Branch", new { id = item.BranchId });
            }
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
            ViewBag.RouteId = item.BranchId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, Item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, Item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, Item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, Item.BranchId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Penant,Name,ShipId,ShipClassId,ShipSubTypeId,StartService,EndService,BranchId,Fate,Active")] ShipService item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.Ships = GetSelectList<ShipView>(ShipsList, item.ShipId);
            ViewBag.ShipClasses = GetSelectList<ShipClassView>(ShipClassesList, item.ShipClassId);
            ViewBag.ShipSubTypes = GetSelectList<ShipSubTypeView>(ShipSubTypesList, item.ShipSubTypeId);
            ViewBag.Branches = GetSelectList<BranchView>(BranchesList, item.BranchId);
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

        protected override DataAccess<ShipService, ShipServiceView> LoadDataAccess()
        {
            return new DataAccess<ShipService, ShipServiceView>(Context, Context.ShipService);
        }

        protected override Func<int, ShipService> GetItemFunction()
        {
            return i => Context.ShipService
                        .Include(x => x.Ship)
                        .Include(x => x.ShipClass)
                        .Include(x => x.ShipSubType)
                        .Include(x => x.Branch)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipService>> GetItemsFunction()
        {
            return () => Context.ShipService
                                .Include(x => x.Ship)
                        .Include(x => x.ShipClass)
                        .Include(x => x.ShipSubType)
                        .Include(x => x.Branch);
        }

        protected override Func<ShipService, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}