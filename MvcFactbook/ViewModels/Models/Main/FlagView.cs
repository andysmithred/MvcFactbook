using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class FlagView : View<Flag>
    {
        private const string FLAG_PATH = "/images/flags/";
        private const string FLAG_EXTENSION = ".png";

        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(100)]
        [Required]
        public string Name => ViewObject.Name;

        [StringLength(6)]
        [Required]
        public string Code => ViewObject.Code;

        public string Description => ViewObject.Description;

        [Display(Name = "Start Date")]
        public DateTime? StartDate => ViewObject.StartDate;

        [Display(Name = "End Date")]
        public DateTime? EndDate => ViewObject.EndDate;

        [Required]
        public bool Active => ViewObject.Active;

        #endregion Database Properties

        #region Foreign Properties

        //public ContinentView Parent => GetView<ContinentView, Continent>(ViewObject.Parent);
        //public ICollection<ContinentView> Children => GetViewList<ContinentView, Continent>(ViewObject.Children);

        //public ICollection<TerritoryView> Territories => GetViewList<TerritoryView, Territory>(ViewObject.Territories);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + " : " + Code;

        public string Image => Code + FLAG_EXTENSION;

        public string ImageSource => FLAG_PATH + Image;

        //public int TerritoryCount => Territories.Count;

        public string StartDateLabel => GetDateLabel(StartDate);

        public string EndDateLabel => GetDateLabel(EndDate);





        #endregion Other Properties

        #region Methods

        private string GetDateLabel(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd MMMM yyyy");
            }
            else
            {
                return "--";
            }
        }


        #endregion Methods


    }
}
