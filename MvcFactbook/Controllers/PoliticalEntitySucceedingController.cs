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
    public class PoliticalEntitySucceedingController : FactbookController<PoliticalEntitySucceeding, PoliticalEntitySucceedingView>
    {
        #region Private Declarations

        private DataAccess<PoliticalEntity, PoliticalEntityView> politicalEntities = null;
        private ICollection<PoliticalEntityView> politicalEntitiesList = null;

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

        #endregion Public Properties

        #region Constructor

        public PoliticalEntitySucceedingController(FactbookContext context)
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
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliticalEntityId,SucceedingPoliticalEntityId,Year")] PoliticalEntitySucceeding item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.SucceedingPoliticalEntityId);
            return View(item);
        }

        public IActionResult CreatePrecedingPoliticalEntity(int id)
        {
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePrecedingPoliticalEntity([Bind("PoliticalEntityId,SucceedingPoliticalEntityId,Year")] PoliticalEntitySucceeding item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntity", new { id = item.SucceedingPoliticalEntityId });
            }
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.SucceedingPoliticalEntityId);
            ViewBag.RouteId = item.SucceedingPoliticalEntityId;
            return View(item);
        }

        public IActionResult CreateSucceedingPoliticalEntity(int id)
        {
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSucceedingPoliticalEntity([Bind("PoliticalEntityId,SucceedingPoliticalEntityId,Year")] PoliticalEntitySucceeding item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntity", new { id = item.PoliticalEntityId });
            }
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.SucceedingPoliticalEntityId);
            ViewBag.RouteId = item.PoliticalEntityId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.PoliticalEntityId);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.SucceedingPoliticalEntityId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,PoliticalEntityId,SucceedingPoliticalEntityId,Year")] PoliticalEntitySucceeding item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PrecedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.SucceedingEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.SucceedingPoliticalEntityId);
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

        protected override DataAccess<PoliticalEntitySucceeding, PoliticalEntitySucceedingView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntitySucceeding, PoliticalEntitySucceedingView>(Context, Context.PoliticalEntitySucceeding);
        }

        protected override Func<int, PoliticalEntitySucceeding> GetItemFunction()
        {
            return i => Context
                        .PoliticalEntitySucceeding
                        .Include(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
                        .Include(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntitySucceeding>> GetItemsFunction()
        {
            return () => Context
                            .PoliticalEntitySucceeding
                            .Include(x => x.PrecedingPoliticalEntity)
                            .Include(x => x.SucceedingPoliticalEntity);
        }

        protected override Func<PoliticalEntitySucceeding, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}