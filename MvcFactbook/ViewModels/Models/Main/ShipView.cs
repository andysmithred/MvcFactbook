using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipView : View<Ship>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Name")]
        public string Name => ViewObject.Name;

        [DataType(DataType.Date)]
        [Display(Name = "Launched")]
        public DateTime? Launched => ViewObject.Launched;

        [Display(Name = "Dockyard Id")]
        public int? DockyardId => ViewObject.DockyardId;

        [Required]
        [Display(Name = "Complete")]
        public bool Complete => ViewObject.Complete;

        #endregion Database Properties

        #region Foreign Properties

        public DockyardView Dockyard => GetView<DockyardView, Dockyard>(ViewObject.Dockyard);

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Year;

        public string Year => Launched.HasValue ? Launched.Value.Year.ToString() : "--";

        public string LaunchedLabel => CommonFunctions.GetDateLabel(Launched);

        [Display(Name = "Years Ago")]
        public TimeSpan? TimeSpan => CommonFunctions.GetTimepan(Launched);

        public string TimeSpanLabel => CommonFunctions.Format(TimeSpan);

        public ICollection<ShipSubTypeView> ShipSubTypes => ShipServices.Select(f => f.ShipSubType).Distinct(f => f.Id).ToList();

        public ICollection<ShipTypeView> ShipTypes => ShipSubTypes.Select(f => f.ShipType).Distinct(f => f.Id).ToList();

        public ICollection<ShipCategoryView> ShipCategories => ShipTypes.Select(f => f.ShipCategory).Distinct(f => f.Id).ToList();

        public ICollection<BranchView> Branches => ShipServices.Select(f => f.Branch).Distinct(f => f.Id).ToList();

        public ICollection<ShipClassView> ShipClasses => ShipServices.Select(f => f.ShipClass).Distinct(f => f.Id).ToList();

        public bool HasFlag => HasDockyard ? Flag != null : false;

        public FlagView Flag => Launched.HasValue ? Dockyard.GetFlagForDate(Launched.Value) : null;

        public bool HasDockyard => Dockyard != null;

        public bool HasShipbuilder => Shipbuilder != null;

        public ShipbuilderView Shipbuilder => HasDockyard & Launched.HasValue ? Dockyard.GetShipbuilderForYear(Launched.Value.Year) : null;

        #endregion Other Properties

        #region Methods

        private TimeSpan GetCareerTimepan()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
