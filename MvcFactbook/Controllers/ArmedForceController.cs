using Microsoft.AspNetCore.Mvc;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public class ArmedForceController : FactbookController<ArmedForce, ArmedForceView>
    {
        #region Constructor

        public ArmedForceController(FactbookContext context)
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
            return base.Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,IsActive,Budget")] ArmedForce item)
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
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,IsActive,Budget")] ArmedForce item)
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

        protected override DataAccess<ArmedForce, ArmedForceView> LoadDataAccess()
        {
            return new DataAccess<ArmedForce, ArmedForceView>(Context, Context.ArmedForce);
        }

        protected override Func<int, ArmedForce> GetItemFunction()
        {
            return i => Context.ArmedForce
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<ArmedForce>> GetItemsFunction()
        {
            return () => Context.ArmedForce;
        }

        protected override Func<ArmedForce, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}