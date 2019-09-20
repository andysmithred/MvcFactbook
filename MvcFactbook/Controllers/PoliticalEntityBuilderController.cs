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
    //public class PoliticalEntityBuilderController : FactbookController<PoliticalEntityBuilder, PoliticalEntityBuilderView>
    //{
    //    #region Private Declarations

    //    private DataAccess<PoliticalEntity, PoliticalEntityView> politicalEntities = null;
    //    private ICollection<PoliticalEntityView> politicalEntitiesList = null;

    //    private DataAccess<Dockyard, DockyardView> dockyards = null;
    //    private ICollection<DockyardView> dockyardsList = null;

    //    #endregion Private Declarations

    //    #region Public Properties

    //    public DataAccess<PoliticalEntity, PoliticalEntityView> PoliticalEntities
    //    {
    //        get => politicalEntities ?? (politicalEntities = new DataAccess<PoliticalEntity, PoliticalEntityView>(Context, Context.PoliticalEntity));
    //        set => politicalEntities = value;
    //    }

    //    public ICollection<PoliticalEntityView> PoliticalEntitiesList
    //    {
    //        get => politicalEntitiesList ?? (politicalEntitiesList = PoliticalEntities.GetViews(() => Context.PoliticalEntity.Include(x => x.PoliticalEntityEras)).OrderBy(x => x.ListName).ToList());
    //        set => politicalEntitiesList = value;
    //    }

    //    public DataAccess<Dockyard, DockyardView> Dockyards
    //    {
    //        get => dockyards ?? (dockyards = new DataAccess<Dockyard, DockyardView>(Context, Context.Dockyard));
    //        set => dockyards = value;
    //    }

    //    public ICollection<DockyardView> DockyardsList
    //    {
    //        get => dockyardsList ?? (dockyardsList = Dockyards.GetViews().OrderBy(x => x.ListName).ToList());
    //        set => dockyardsList = value;
    //    }

    //    #endregion Public Properties

    //    #region Constructor

    //    public PoliticalEntityBuilderController(FactbookContext context)
    //        : base(context) { }

    //    #endregion Constructor

    //    #region Index

    //    public override async Task<IActionResult> Index()
    //    {
    //        return await base.Index();
    //    }

    //    #endregion Index

    //    #region Details

    //    public override async Task<IActionResult> Details(int? id)
    //    {
    //        return await base.Details(id);
    //    }

    //    #endregion Details

    //    #region Create

    //    public override IActionResult Create()
    //    {
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
    //        ViewBag.Dockyards = GetSelectList<DockyardView>(DockyardsList, null);
    //        return base.Create();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("PoliticalEntityId,DockyardId")] PoliticalEntityBuilder item)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await AddAsync(item);
    //            return RedirectToAction("Details", new { id = item.Id });
    //        }
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
    //        return View(item);
    //    }

    //    public IActionResult CreateByPoliticalEntity(int id)
    //    {
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, id);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, null);
    //        ViewBag.RouteId = id;
    //        return base.Create();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> CreateByPoliticalEntity([Bind("PoliticalEntityId,BuilderId")] PoliticalEntityBuilder item)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await AddAsync(item);
    //            return RedirectToAction("Details", "PoliticalEntity", new { id = item.PoliticalEntityId });
    //        }
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
    //        ViewBag.RouteId = item.PoliticalEntityId;
    //        return View(item);
    //    }

    //    public IActionResult CreateByBuilder(int id)
    //    {
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, null);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, id);
    //        ViewBag.RouteId = id;
    //        return base.Create();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> CreateByBuilder([Bind("PoliticalEntityId,BuilderId")] PoliticalEntityBuilder item)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await AddAsync(item);
    //            return RedirectToAction("Details", "Builder", new { id = item.BuilderId });
    //        }
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
    //        ViewBag.RouteId = item.BuilderId;
    //        return View(item);
    //    }

    //    #endregion Create

    //    #region Edit

    //    public override async Task<IActionResult> Edit(int? id)
    //    {
    //        IActionResult result = await base.Edit(id);
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, Item.PoliticalEntityId);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, Item.BuilderId);
    //        return result;
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public override async Task<IActionResult> Edit(int id, [Bind("Id,PoliticalEntityId,BuilderId")] PoliticalEntityBuilder item)
    //    {
    //        IActionResult result = await base.Edit(id, item);
    //        ViewBag.PoliticalEntities = GetSelectList<PoliticalEntityView>(PoliticalEntitiesList, item.PoliticalEntityId);
    //        ViewBag.Builders = GetSelectList<BuilderView>(BuildersList, item.BuilderId);
    //        return result;
    //    }

    //    #endregion Edit

    //    #region Delete

    //    public override async Task<IActionResult> Delete(int? id)
    //    {
    //        return await base.Delete(id);
    //    }

    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public override async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        return await base.DeleteConfirmed(id);
    //    }

    //    #endregion Delete

    //    #region Override Abstract Methods

    //    protected override DataAccess<PoliticalEntityBuilder, PoliticalEntityBuilderView> LoadDataAccess()
    //    {
    //        return new DataAccess<PoliticalEntityBuilder, PoliticalEntityBuilderView>(Context, Context.PoliticalEntityBuilder);
    //    }

    //    protected override Func<int, PoliticalEntityBuilder> GetItemFunction()
    //    {
    //        return i => Context.PoliticalEntityBuilder
    //                    .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
    //                    .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityFlags).ThenInclude(x => x.Flag)
    //                    .Include(x => x.Builder)//.ThenInclude(x => x.Ships)
    //                    .FirstOrDefault(x => x.Id == i);
    //    }

    //    protected override Func<IQueryable<PoliticalEntityBuilder>> GetItemsFunction()
    //    {
    //        return () => Context.PoliticalEntityBuilder
    //                            .Include(x => x.PoliticalEntity).ThenInclude(x => x.PoliticalEntityType)
    //                            .Include(x => x.Builder);
    //    }

    //    protected override Func<PoliticalEntityBuilder, bool> GetExistsFunc(int id)
    //    {
    //        return i => i.Id == id;
    //    }

    //    #endregion Override Abstract Methods
    //}
}
