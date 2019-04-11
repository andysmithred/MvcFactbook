using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BuilderView : View<Builder>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        public int? Founded => ViewObject.Founded;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipView> Ships => GetViewList<ShipView, Ship>(ViewObject.Ships);

        public ICollection<PoliticalEntityBuilderView> PoliticalEntityBuilders => GetViewList<PoliticalEntityBuilderView, PoliticalEntityBuilder>(ViewObject.PoliticalEntityBuilders);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name;

        public string FoundedLabel => Founded.HasValue ? Founded.Value.ToString() : "--";

        public ICollection<PoliticalEntityView> PoliticalEntities => PoliticalEntityBuilders.Select(f => f.PoliticalEntity).Distinct(f => f.Id).ToList();




       
        public bool HasFlag => PoliticalEntities.Count > 0;

        public FlagView Flag => PoliticalEntities.OrderByDescending(x => x.StartDate).FirstOrDefault().CurrentFlag;

        public string ImageSource => Flag?.ImageSource;


        #endregion Other Properties

    }
}
