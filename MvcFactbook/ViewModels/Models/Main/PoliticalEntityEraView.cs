using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityEraView : View<PoliticalEntityEra>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId => ViewObject.PoliticalEntityId;

        [Display(Name = "Start Date")]
        public DateTime? StartDate => ViewObject.StartDate;

        [Display(Name = "End Date")]
        public DateTime? EndDate => ViewObject.EndDate;

        public string Description => ViewObject.Description;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntityView PoliticalEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.PoliticalEntity);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PoliticalEntity.ShortName + ":" + StartDateLabel + " - " + EndDateLabel;

        public string StartDateLabel => CommonFunctions.GetDateLabel(StartDate);

        public string EndDateLabel => CommonFunctions.GetDateLabel(EndDate);

        public DateTime AbsoluteStart => StartDate.HasValue ? StartDate.Value : DateTime.MinValue;

        public DateTime AbsoluteEnd => EndDate.HasValue ? EndDate.Value : DateTime.MaxValue;

        [Display(Name = "Time Span")]
        public TimeSpan? TimeSpan => CommonFunctions.GetTimepan(StartDate, EndDate);

        public string TimeSpanLabel => CommonFunctions.Format(TimeSpan);

        public string Years => CommonFunctions.GetYears(StartDate, EndDate);

        #endregion Other Properties

    }
}
