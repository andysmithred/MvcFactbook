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
    public class FlagController : FactbookController<Flag, FlagView>
    {
        #region Constructor

        public FlagController(FactbookContext context)
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

        public async Task<IActionResult> DetailsPoliticalEntities(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsArmedForces(int? id)
        {
            return await base.Details(id);
        }

        public async Task<IActionResult> DetailsBranches(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Description,StartDate,EndDate,Active,Complete")] Flag item)
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
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Description,StartDate,EndDate,Active,Complete")] Flag item)
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

        protected override DataAccess<Flag, FlagView> LoadDataAccess()
        {
            return new DataAccess<Flag, FlagView>(Context, Context.Flag);
        }

        protected override Func<int, Flag> GetItemFunction()
        {
            return i => Context.Flag
                        .Include(x => x.ArmedForceFlags).ThenInclude(x => x.ArmedForce)
                        .Include(x => x.BranchFlags).ThenInclude(x => x.Branch)
                        .Include(x => x.PoliticalEntityFlags).ThenInclude(x => x.PoliticalEntity)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<Flag>> GetItemsFunction()
        {
            return () => Context.Flag
                            .Include(x => x.ArmedForceFlags).ThenInclude(x => x.ArmedForce);
        }

        protected override Func<Flag, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}