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
    public class PoliticalEntityDockyardController : FactbookController<PoliticalEntityDockyard, PoliticalEntityDockyardView>
    {
        #region Private Declarations

        private DataAccess<PoliticalEntity, PoliticalEntityView> politicalEntities = null;
        private ICollection<PoliticalEntityView> politicalEntitiesList = null;

        private DataAccess<Dockyard, DockyardView> dockyards = null;
        private ICollection<DockyardView> dockyardsList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<PoliticalEntity, PoliticalEntityView> PoliticalEntities
        {
            get => politicalEntities ?? (politicalEntities = new DataAccess<PoliticalEntity, PoliticalEntityView>(Context, Context.PoliticalEntity));
            set => politicalEntities = value;
        }

        public ICollection<PoliticalEntityView> PoliticalEntitiesList
        {
            get => politicalEntitiesList ?? (politicalEntitiesList = PoliticalEntities.GetViews(() => Context.PoliticalEntity.Include(x => x.PoliticalEntityEras)).OrderBy(x => x.ListName).ToList());
            set => politicalEntitiesList = value;
        }

        public DataAccess<Dockyard, DockyardView> Dockyards
        {
            get => dockyards ?? (dockyards = new DataAccess<Dockyard, DockyardView>(Context, Context.Dockyard));
            set => dockyards = value;
        }

        public ICollection<DockyardView> DockyardsList
        {
            get => dockyardsList ?? (dockyardsList = Dockyards.GetViews().OrderBy(x => x.ListName).ToList());
            set => dockyardsList = value;
        }

        #endregion Public Properties

        #region Constructor

        public PoliticalEntityDockyardController(FactbookContext context)
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
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliticalEntityId,DockyardId")] PoliticalEntityDockyard item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            return View(item);
        }

        public IActionResult CreateByPoliticalEntity(int id)
        {
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByPoliticalEntity([Bind("PoliticalEntityId,DockyardId")] PoliticalEntityDockyard item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "PoliticalEntity", new { id = item.PoliticalEntityId });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.RouteId = item.PoliticalEntityId;
            return View(item);
        }

        public IActionResult CreateByDockyard(int id)
        {
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByDockyard([Bind("PoliticalEntityId,DockyardId")] PoliticalEntityDockyard item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Dockyard", new { id = item.DockyardId });
            }
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.RouteId = item.DockyardId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.PoliticalEntityId);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, Item.DockyardId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,PoliticalEntityId,DockyardId")] PoliticalEntityDockyard item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
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

        protected override DataAccess<PoliticalEntityDockyard, PoliticalEntityDockyardView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntityDockyard, PoliticalEntityDockyardView>(Context, Context.PoliticalEntityDockyard);
        }

        protected override Func<int, PoliticalEntityDockyard> GetItemFunction()
        {
            return i => Context
                        .PoliticalEntityDockyard
                        .Include(x => x.PoliticalEntity)
                            .ThenInclude(x => x.PoliticalEntityType)
                        .Include(x => x.PoliticalEntity)
                            .ThenInclude(x => x.PoliticalEntityFlags)
                                .ThenInclude(x => x.Flag)
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.Ships)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntityDockyard>> GetItemsFunction()
        {
            return () => Context
                            .PoliticalEntityDockyard
                            .Include(x => x.PoliticalEntity)
                                .ThenInclude(x => x.PoliticalEntityType)
                            .Include(x => x.Dockyard);
        }

        protected override Func<PoliticalEntityDockyard, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}
