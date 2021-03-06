﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Controllers
{
    public class PoliticalEntityTypeController : FactbookController<PoliticalEntityType, PoliticalEntityTypeView>
    {
        #region Constructor

        public PoliticalEntityTypeController(FactbookContext context)
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
        public async Task<IActionResult> Create([Bind("Id,Type")] PoliticalEntityType item)
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
        public override async Task<IActionResult> Edit(int id, [Bind("Id,Type")] PoliticalEntityType item)
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

        protected override DataAccess<PoliticalEntityType, PoliticalEntityTypeView> LoadDataAccess()
        {
            return new DataAccess<PoliticalEntityType, PoliticalEntityTypeView>(Context, Context.PoliticalEntityType);
        }

        protected override Func<int, PoliticalEntityType> GetItemFunction()
        {
            return i => Context
                        .PoliticalEntityType
                        .Include(x => x.PoliticalEntities).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
                        .FirstOrDefault(x => x.Id == i);
        }

        protected override Func<IQueryable<PoliticalEntityType>> GetItemsFunction()
        {
            return () => Context
                        .PoliticalEntityType;
        }

        protected override Func<PoliticalEntityType, bool> GetExistsFunc(int id)
        {
            return i => i.Id == id;
        }

        #endregion Override Abstract Methods
    }
}