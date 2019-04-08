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
    public class ArmedForceFlagController : FactbookController<ArmedForceFlag, ArmedForceFlagView>
    {
        #region Private Declarations

        private DataAccess<ArmedForce, ArmedForceView> armedForces = null;
        private ICollection<ArmedForceView> armedForcesList = null;

        private DataAccess<Flag, FlagView> flags = null;
        private ICollection<FlagView> flagsList = null;

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

        public ArmedForceFlagController(FactbookContext context)
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
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArmedForceId,FlagId,Start,End")] ArmedForceFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            return View(item);
        }

        public IActionResult CreateByArmedForce(int id)
        {
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, id);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            ViewBag.ArmedForce = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByArmedForce([Bind("ArmedForceId,FlagId,Start,End")] ArmedForceFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "ArmedForce", new { id = item.ArmedForceId });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.ArmedForce = item.ArmedForceId;
            return View(item);
        }

        public IActionResult CreateByFlag(int id)
        {
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFlag([Bind("ArmedForceId,FlagId,Start,End")] ArmedForceFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Flag", new { id = item.FlagId });
            }
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.RouteId = item.FlagId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, Item.ArmedForceId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, Item.FlagId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,ArmedForceId,FlagId,Start,End")] ArmedForceFlag item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.ArmedForces = GetSelectList<ArmedForceView>(ArmedForcesList, item.ArmedForceId);
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

        protected override DataAccess<ArmedForceFlag, ArmedForceFlagView> LoadDataAccess()
        {
            return new DataAccess<ArmedForceFlag, ArmedForceFlagView>(Context, Context.ArmedForceFlag);
        }

        protected override Func<int, ArmedForceFlag> GetItemFunction()
        {
            return i => Context.ArmedForceFlag
                        .Include(x => x.ArmedForce)
                        .Include(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ArmedForceFlag>> GetItemsFunction()
        {
            return () => Context.ArmedForceFlag
                                .Include(x => x.ArmedForce)
                                .Include(x => x.Flag);
        }

        protected override Func<ArmedForceFlag, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}