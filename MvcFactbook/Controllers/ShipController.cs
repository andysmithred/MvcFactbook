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
    public class ShipController : FactbookController<Ship, ShipView>
    {
        #region Private Declarations

        private DataAccess<Dockyard, DockyardView> dockyards = null;
        private ICollection<DockyardView> dockyardsList = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<Dockyard, DockyardView> Builders
        {
            get => dockyards ?? (dockyards = new DataAccess<Dockyard, DockyardView>(Context, Context.Dockyard));
            set => dockyards = value;
        }

        public ICollection<DockyardView> DockyardsList
        {
            get => dockyardsList ?? (dockyardsList = Builders.GetViews().OrderBy(x => x.ListName).ToList());
            set => dockyardsList = value;
        }

        #endregion Public Properties

        #region Constructor

        public ShipController(FactbookContext context)
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

        public async Task<IActionResult> DetailsShipServices(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Launched,DockyardId,Complete")] Ship item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            return View(item);
        }

        public IActionResult CreateByDockyard(int id)
        {
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, id);
            ViewBag.RouteId = id;
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByDockyard([Bind("Name,Launched,DockyardId,Complete")] Ship item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", "Builder", new { id = item.DockyardId });
            }
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, item.DockyardId);
            ViewBag.RouteId = item.DockyardId;
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = await base.Edit(id);
            ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, Item.DockyardId);
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,Launched,DockyardId,Complete")] Ship item)
        {
            IActionResult result = await base.Edit(id, item);
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

        protected override DataAccess<Ship, ShipView> LoadDataAccess()
        {
            return new DataAccess<Ship, ShipView>(Context, Context.Ship);
        }

        protected override Func<int, Ship> GetItemFunction()
        {
            return i => Context.Ship
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.PoliticalEntityDockyards)
                                .ThenInclude(x => x.PoliticalEntity)
                                    .ThenInclude(x => x.PoliticalEntityFlags)
                                        .ThenInclude(x => x.Flag)
                        .Include(x => x.Dockyard)
                            .ThenInclude(x => x.PoliticalEntityDockyards)
                                .ThenInclude(x => x.PoliticalEntity)
                                    .ThenInclude(x => x.PoliticalEntityEras)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.Branch)
                                .ThenInclude(x => x.BranchFlags)
                                    .ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices)
                            .ThenInclude(x => x.ShipClass)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<Ship>> GetItemsFunction()
        {
            return () => Context.Ship
                            .Include(x => x.Dockyard)
                            .Include(x => x.ShipServices)
                                .ThenInclude(x => x.Branch)
                                    .ThenInclude(x => x.BranchFlags)
                                        .ThenInclude(x => x.Flag);
        }

        protected override Func<Ship, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}