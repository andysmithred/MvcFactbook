using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityFlagView : View<PoliticalEntityFlag>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId => ViewObject.PoliticalEntityId;

        [Required]
        [Display(Name = "Flag Id")]
        public int FlagId => ViewObject.FlagId;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate => ViewObject.StartDate;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate => ViewObject.EndDate;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntityView PoliticalEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.PoliticalEntity);
        public FlagView Flag => GetView<FlagView, Flag>(ViewObject.Flag);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PoliticalEntity.ShortName + ":" + Flag.Name;

        public string StartDateLabel => CommonFunctions.GetDateLabel(StartDate);

        public string EndDateLabel => CommonFunctions.GetDateLabel(EndDate);

        public DateTime AbsoluteStart => StartDate.HasValue ? StartDate.Value : DateTime.MinValue;

        public DateTime AbsoluteEnd => EndDate.HasValue ? EndDate.Value : DateTime.MaxValue;

        #endregion Other Properties

        #region Methods



        #endregion Methods
    }
}
