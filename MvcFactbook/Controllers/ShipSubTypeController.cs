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
    public class ShipSubTypeController : FactbookController<ShipSubType, ShipSubTypeView>
    {
        #region Private Declarations

        private DataAccess<ShipType, ShipTypeView> shipTypes = null;
        private ICollection<ShipTypeView> shipTypesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ShipType, ShipTypeView> ShipTypes
        {
            get => shipTypes ?? (shipTypes = new DataAccess<ShipType, ShipTypeView>(Context, Context.ShipType));
            set => shipTypes = value;
        }

        public ICollection<ShipTypeView> ShipTypesList
        {
            get => shipTypesList ?? (shipTypesList = ShipTypes.GetViews().OrderBy(x => x.Type).ToList());
            set => shipTypesList = value;
        }

        #endregion Public Properties

        #region Constructor

        public ShipSubTypeController(FactbookContext context)
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

        public async Task<IActionResult> BranchesList(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,ShipTypeId")] ShipSubType item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, item.ShipTypeId);
            return View(item);
        }

        public IActionResult CreateByShipType(int id)
        {
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipType([Bind("Type,ShipTypeId")] ShipSubType item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ShipType", new { id = item.ShipTypeId });
            }
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, item.ShipTypeId);
            ViewBag.RouteId = item.ShipTypeId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, Item.ShipTypeId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Type,ShipTypeId")] ShipSubType item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.ShipTypes = GetSelectList<ShipTypeView>(ShipTypesList, item.ShipTypeId);
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

        protected override DataAccess<ShipSubType, ShipSubTypeView> LoadDataAccess()
        {
            return new DataAccess<ShipSubType, ShipSubTypeView>(Context, Context.ShipSubType);
        }

        protected override Func<int, ShipSubType> GetItemFunction()
        {
            return i => Context.ShipSubType
                        .Include(x => x.ShipType).ThenInclude(x => x.ShipCategory)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.ArmedForce)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchType)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipClass)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipSubType>> GetItemsFunction()
        {
            return () => Context.ShipSubType
                                .Include(x => x.ShipType).ThenInclude(x => x.ShipCategory);
        }

        protected override Func<ShipSubType, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}