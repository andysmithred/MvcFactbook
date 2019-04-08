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
    public class PoliticalEntityFlagController : FactbookController<PoliticalEntityFlag, PoliticalEntityFlagView>
    {
        #region Private Declarations

        private DataAccess<PoliticalEntity, PoliticalEntityView> politicalEntities = null;
        private ICollection<PoliticalEntityView> politicalEntitiesList = null;

        private DataAccess<Flag, FlagView> flags = null;
        private ICollection<FlagView> flagsList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<PoliticalEntity, PoliticalEntityView> PoliticalEntities
        {
            get => politicalEntities ?? (politicalEntities = new DataAccess<PoliticalEntity, PoliticalEntityView>(Context, Context.PoliticalEntity));
            set => politicalEntities = value;
        }

        public ICollection<PoliticalEntityView> PoliticalEntitiesList
        {
            get => politicalEntitiesList ?? (politicalEntitiesList = PoliticalEntities.GetViews().OrderBy(x => x.ListName).ToList());
            set => politicalEntitiesList = value;
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

        public PoliticalEntityFlagController(FactbookContext context)
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
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PoliticalEntityId,FlagId,StartDate,EndDate")] PoliticalEntityFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            return View(item);
        }

        public IActionResult CreateByPoliticalEntity(int id)
        {
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByPoliticalEntity([Bind("Id,PoliticalEntityId,FlagId,StartDate,EndDate")] PoliticalEntityFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntity", new { id = item.PoliticalEntityId });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.RouteId = item.PoliticalEntityId;
            return View(item);
        }

        public IActionResult CreateByFlag(int id)
        {
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFlag([Bind("Id,PoliticalEntityId,FlagId,StartDate,EndDate")] PoliticalEntityFlag item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Flag", new { id = item.FlagId });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, item.FlagId);
            ViewBag.RouteId = item.FlagId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.PoliticalEntityId);
            ViewBag.Flags = GetSelectList<FlagView>(FlagsList, Item.FlagId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,PoliticalEntityId,FlagId,StartDate,EndDate")] PoliticalEntityFlag item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
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

        protected override DataAccess<PoliticalEntityFlag, PoliticalEntityFlagView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntityFlag, PoliticalEntityFlagView>(Context, Context.PoliticalEntityFlag);
        }

        protected override Func<int, PoliticalEntityFlag> GetItemFunction()
        {
            return i => Context.PoliticalEntityFlag
                        .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
                        .Include(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntityFlag>> GetItemsFunction()
        {
            return () => Context.PoliticalEntityFlag
                                .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
                                .Include(x => x.Flag);
        }

        protected override Func<PoliticalEntityFlag, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}
