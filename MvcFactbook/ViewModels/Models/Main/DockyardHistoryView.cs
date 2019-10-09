using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class DockyardHistoryView : View<DockyardHistory>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Dockyard Id")]
        public int DockyardId => ViewObject.DockyardId;

        [Required]
        [Display(Name = "Shipbuilder Id")]
        public int ShipbuilderId => ViewObject.ShipbuilderId;

        [Display(Name = "Start")]
        public int? Start => ViewObject.Start;

        [Display(Name = "End")]
        public int? End => ViewObject.End;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Dockyard")]
        public DockyardView Dockyard => GetView<DockyardView, Dockyard>(ViewObject.Dockyard);

        [Display(Name = "Shipbuilder")]
        public ShipbuilderView Shipbuilder => GetView<ShipbuilderView, Shipbuilder>(ViewObject.Shipbuilder);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Dockyard.Name + "|" + Shipbuilder.Name;

        public string StartLabel => Start.HasValue ? Start.Value.ToString() : "--";

        public string EndLabel => End.HasValue ? End.Value.ToString() : "--";

        public int AbsoluteStart => Start.HasValue ? Start.Value : DateTime.MinValue.Year;

        public int AbsoluteEnd => End.HasValue ? End.Value : DateTime.MaxValue.Year;

        #endregion Other Properties
    }
}
