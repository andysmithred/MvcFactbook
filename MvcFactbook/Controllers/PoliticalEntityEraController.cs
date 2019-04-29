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
    public class PoliticalEntityEraController : FactbookController<PoliticalEntityEra, PoliticalEntityEraView>
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

        public PoliticalEntityEraController(FactbookContext context)
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
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliticalEntityId,StartDate,EndDate,Description")] PoliticalEntityEra item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            return View(item);
        }

        public IActionResult CreateByPoliticalEntity(int id)
        {
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByPoliticalEntity([Bind("PoliticalEntityId,StartDate,EndDate,Description")] PoliticalEntityEra item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntity", new { id = item.PoliticalEntityId });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.RouteId = item.PoliticalEntityId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.PoliticalEntityId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,PoliticalEntityId,StartDate,EndDate,Description")] PoliticalEntityEra item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
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

        protected override DataAccess<PoliticalEntityEra, PoliticalEntityEraView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntityEra, PoliticalEntityEraView>(Context, Context.PoliticalEntityEra);
        }

        protected override Func<int, PoliticalEntityEra> GetItemFunction()
        {
            return i => Context.PoliticalEntityEra
                        .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntityEra>> GetItemsFunction()
        {
            return () => Context.PoliticalEntityEra
                                .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType);
        }

        protected override Func<PoliticalEntityEra, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}
