using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public class ShipClassController : FactbookController<ShipClass, ShipClassView>
    {
        #region Constructor

        public ShipClassController(FactbookContext context)
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

        public async Task<IActionResult> DetailsShips(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsShipServices(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsPrecedingClasses(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsSucceedingClasses(int? id)
        {
            return await base.Details(id);
        }

        #endregion Details

        #region Create

        public override IActionResult Create()
        {
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SubClassName,Displacement,Length,Beam,Propulsion,Speed,Crew,Year")] ShipClass item)
        {
            if (ModelState.IsValid)
            {
                await AddAsync(item);
                return RedirectToAction("Details", new { id = item.Id });
            }
            return View(item);
        }

        #endregion Create

        #region Edit

        public override async Task<IActionResult> Edit(int? id)
        {
            return await base.Edit(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,SubClassName,Displacement,Length,Beam,Propulsion,Speed,Crew,Year")] ShipClass item)
        {
            return await base.Edit(id, item);
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

        protected override DataAccess<ShipClass, ShipClassView> LoadDataAccess()
        {
            return new DataAccess<ShipClass, ShipClassView>(Context, Context.ShipClass);
        }

        protected override Func<int, ShipClass> GetItemFunction()
        {
            return i => Context.ShipClass
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship).ThenInclude(x => x.Builder)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ShipClass>> GetItemsFunction()
        {
            return () => Context.ShipClass
                        .Include(x => x.ShipServices).ThenInclude(x => x.Branch).ThenInclude(x => x.BranchFlags).ThenInclude(x => x.Flag)
                        .Include(x => x.ShipServices).ThenInclude(x => x.ShipSubType)
                        .Include(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.PrecedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.PrecedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship)
                        .Include(x => x.SucceedingClasses).ThenInclude(x => x.SucceedingShipClass).ThenInclude(x => x.ShipServices).ThenInclude(x => x.Ship);
        }

        protected override Func<ShipClass, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}