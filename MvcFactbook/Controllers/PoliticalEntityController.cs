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
    public class PoliticalEntityController : FactbookController<PoliticalEntity, PoliticalEntityView>
    {
        #region Private Declarations

        private DataAccess<PoliticalEntityType, PoliticalEntityTypeView> politicalEntityTypes = null;
        private ICollection<PoliticalEntityTypeView> politicalEntityTypesList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<PoliticalEntityType, PoliticalEntityTypeView> PoliticalEntityTypes
        {
            get => politicalEntityTypes ?? (politicalEntityTypes = new DataAccess<PoliticalEntityType, PoliticalEntityTypeView>(Context, Context.PoliticalEntityType));
            set => politicalEntityTypes = value;
        }

        public ICollection<PoliticalEntityTypeView> PoliticalEntityTypesList
        {
            get => politicalEntityTypesList ?? (politicalEntityTypesList = PoliticalEntityTypes.GetViews().OrderBy(x => x.ListName).ToList());
            set => politicalEntityTypesList = value;
        }

        #endregion Public Properties

        #region Constructor

        public PoliticalEntityController(FactbookContext context)
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

        public async Task<IActionResult> DetailsFlags(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsPrecedingEntities(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsSucceedingEntities(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsBuilders(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShortName,Name,FullName,Code,StartDate,EndDate,Exists,HasEmblem,PoliticalEntityTypeId,Complete")] PoliticalEntity item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, item.PoliticalEntityTypeId);
            return View(item);
        }

        public IActionResult CreateByPoliticalEntityType(int id)
        {
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByPoliticalEntityType([Bind("ShortName,Name,FullName,Code,StartDate,EndDate,Exists,HasEmblem,PoliticalEntityTypeId,Complete")] PoliticalEntity item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntityType", new { id = item.PoliticalEntityTypeId });
            }
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, item.PoliticalEntityTypeId);
            ViewBag.RouteId = item.PoliticalEntityTypeId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, Item.PoliticalEntityTypeId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,ShortName,Name,FullName,Code,StartDate,EndDate,Exists,HasEmblem,PoliticalEntityTypeId,Complete")] PoliticalEntity item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PoliticalEntityTypes = GetSelectList<PoliticalEntityTypeView>(PoliticalEntityTypesList, item.PoliticalEntityTypeId);
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

        protected override DataAccess<PoliticalEntity, PoliticalEntityView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntity, PoliticalEntityView>(Context, Context.PoliticalEntity);
        }

        protected override Func<int, PoliticalEntity> GetItemFunction()
        {
            return i => Context
                        .PoliticalEntity
                        .Include(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PrecedingEntities).ThenInclude(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PrecedingEntities).ThenInclude(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.SucceedingEntities).ThenInclude(x => x.PrecedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.SucceedingEntities).ThenInclude(x => x.SucceedingPoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityDockyards).ThenInclude(x => x.Dockyard).ThenInclude(x => x.Ships)
                        .Include(x => x.PoliticalEntityEras)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntity>> GetItemsFunction()
        {
            return () => Context
                        .PoliticalEntity
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntityEras);
        }

        protected override Func<PoliticalEntity, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}