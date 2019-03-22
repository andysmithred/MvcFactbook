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
    public class ShipGroupSetController : FactbookController<ShipGroupSet, ShipGroupSetView>
    {
        #region Private Declarations

        private DataAccess<ShipService, ShipServiceView> shipServices = null;
        private ICollection<ShipServiceView> shipServicesList = null;

        private DataAccess<ShipGroup, ShipGroupView> shipGroups = null;
        private ICollection<ShipGroupView> shipGroupsList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ShipService, ShipServiceView> ShipServices
        {
            get => shipServices ?? (shipServices = new DataAccess<ShipService, ShipServiceView>(Context, Context.ShipService));
            set => shipServices = value;
        }

        public ICollection<ShipServiceView> ShipServicesList
        {
            get => shipServicesList ?? (shipServicesList = ShipServices.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipServicesList = value;
        }

        public DataAccess<ShipGroup, ShipGroupView> ShipGroups
        {
            get => shipGroups ?? (shipGroups = new DataAccess<ShipGroup, ShipGroupView>(Context, Context.ShipGroup));
            set => shipGroups = value;
        }

        public ICollection<ShipGroupView> ShipGroupsList
        {
            get => shipGroupsList ?? (shipGroupsList = ShipGroups.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipGroupsList = value;
        }

        #endregion Public Properties

        #region Constructor

        public ShipGroupSetController(FactbookContext context)
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
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, null);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShipServiceId,ShipGroupId")] ShipGroupSet item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, item.ShipServiceId);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, item.ShipGroupId);
            return View(item);
        }

        public IActionResult CreateByShipService(int id)
        {
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, id);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipService([Bind("Id,ShipServiceId,ShipGroupId")] ShipGroupSet item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipService", new { id = item.ShipServiceId });
            }
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, item.ShipServiceId);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, item.ShipGroupId);
            ViewBag.RouteId = item.ShipServiceId;
            return View(item);
        }

        public IActionResult CreateByShipGroup(int id)
        {
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, null);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipGroup([Bind("Id,ShipServiceId,ShipGroupId")] ShipGroupSet item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipGroup", new { id = item.ShipGroupId });
            }
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, item.ShipServiceId);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, item.ShipGroupId);
            ViewBag.RouteId = item.ShipGroupId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, Item.ShipServiceId);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, Item.ShipGroupId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,ShipServiceId,ShipGroupId")] ShipGroupSet item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.ShipServices = GetSelectList<ShipServiceView>(ShipServicesList, item.ShipServiceId);
            ViewBag.ShipGroups = GetSelectList<ShipGroupView>(ShipGroupsList, item.ShipGroupId);
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

        protected override DataAccess<ShipGroupSet, ShipGroupSetView> LoadDataAccess()
        {
            return new DataAccess<ShipGroupSet, ShipGroupSetView>(Context, Context.ShipGroupSet);
        }

        protected override Func<int, ShipGroupSet> GetItemFunction()
        {
            return i => Context
                        .ShipGroupSet
                        .Include(x => x.ShipService).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipGroup)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipGroupSet>> GetItemsFunction()
        {
            return () => Context
                        .ShipGroupSet
                        .Include(x => x.ShipService)
                        .Include(x => x.ShipGroup);
        }

        protected override Func<ShipGroupSet, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}