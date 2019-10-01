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
    public class DockyardHistoryController : FactbookController<DockyardHistory, DockyardHistoryView>
    {
        #region Private Declarations

        private DataAccess<Dockyard, DockyardView> dockyards = null;
        private ICollection<DockyardView> dockyardsList = null;

        private DataAccess<Shipbuilder, ShipbuilderView> shipbuilders = null;
        private ICollection<ShipbuilderView> shipbuildersList = null;

        #endregion Private Declarations

        #region Public Properties

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

        public DataAccess<Shipbuilder, ShipbuilderView> Shipbuilders
        {
            get => shipbuilders ?? (shipbuilders = new DataAccess<Shipbuilder, ShipbuilderView>(Context, Context.Shipbuilder));
            set => shipbuilders = value;
        }

        public ICollection<ShipbuilderView> ShipbuildersList
        {
            get => shipbuildersList ?? (shipbuildersList = Shipbuilders.GetViews().OrderBy(x => x.ListName).ToList());
            set => shipbuildersList = value;
        }

        #endregion Public Properties

        #region Constructor

        public DockyardHistoryController(FactbookContext context)
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
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DockyardId,ShipbuilderId,Start,End")] DockyardHistory item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, item.ShipbuilderId);
            return View(item);
        }

        public IActionResult CreateByDockyard(int id)
        {
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, id);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, null);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByDockyard([Bind("DockyardId,ShipbuilderId,Start,End")] DockyardHistory item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Dockyard", new { id = item.DockyardId });
            }
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, item.ShipbuilderId);
            ViewBag.RouteId = item.DockyardId;
            return View(item);
        }

        public IActionResult CreateByShipbuilder(int id)
        {
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByShipbuilder([Bind("DockyardId,ShipbuilderId,Start,End")] DockyardHistory item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Shipbuilder", new { id = item.ShipbuilderId });
            }
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, item.ShipbuilderId);
            ViewBag.RouteId = item.ShipbuilderId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, Item.DockyardId);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, Item.ShipbuilderId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,DockyardId,ShipbuilderId,Start,End")] DockyardHistory item)
        {
            IActionResult result = await base.Edit(id, item);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.Shipbuilders = GetSelectList<ShipbuilderView>(ShipbuildersList, item.ShipbuilderId);
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

        protected override DataAccess<DockyardHistory, DockyardHistoryView> LoadDataAccess()
        {
            return new DataAccess<DockyardHistory, DockyardHistoryView>(Context, Context.DockyardHistory);
        }

        protected override Func<int, DockyardHistory> GetItemFunction()
        {
            return i => Context
                        .DockyardHistory
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.Ships)
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.PoliticalEntityDockyards)
                                .ThenInclude(x => x.PoliticalEntity)
                                    .ThenInclude(x => x.PoliticalEntityFlags)
                                        .ThenInclude(x => x.Flag)
                        .Include(x => x.Shipbuilder)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<DockyardHistory>> GetItemsFunction()
        {
            return () => Context
                        .DockyardHistory
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.Ships)
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.PoliticalEntityDockyards)
                                .ThenInclude(x => x.PoliticalEntity)
                                    .ThenInclude(x => x.PoliticalEntityFlags)
                                        .ThenInclude(x => x.Flag)
                        .Include(x => x.Shipbuilder);
        }

        protected override Func<DockyardHistory, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}