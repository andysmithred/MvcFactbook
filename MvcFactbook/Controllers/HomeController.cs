using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;

namespace MvcFactbook.Controllers
{
    public class HomeController : Controller
    {
        #region Private Declarations

        private DataAccess<ShipClass, ShipClassView> shipClasses = null;
        private int? shipClassTotal = null;
        private int? shipClassComplete = null;
        private int? shipClassIncomplete = null;

        private FactbookContext context = null;

        #endregion Private Declarations

        #region Public Properties

        public DataAccess<ShipClass, ShipClassView> ShipClasses
        {
            get => shipClasses ?? (shipClasses = new DataAccess<ShipClass, ShipClassView>(Context, Context.ShipClass));
            set => shipClasses = value;
        }

        public int? ShipClassTotal
        {
            get => shipClassTotal ?? (shipClassTotal = ShipClasses.Count());
            set => shipClassTotal = value;
        }

        public int? ShipClassComplete
        {
            get => shipClassComplete ?? (shipClassComplete = ShipClasses.Count(x => x.Complete == true));
            set => shipClassComplete = value;
        }

        public int? ShipClassIncomplete
        {
            get => shipClassIncomplete ?? (shipClassIncomplete = ShipClasses.Count(x => x.Complete == false));
            set => shipClassIncomplete = value;
        }


        #endregion Public Properties



        public HomeController(FactbookContext context)
        {
            Context = context;
        }

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The Factbook Context object has not been set.");
            set => context = value;
        }




        public IActionResult Index()
        {

            ViewBag.Total = ShipClassTotal.Value;
            ViewBag.Complete = ShipClassComplete.Value;
            ViewBag.Incomplete = ShipClassIncomplete.Value;




            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
