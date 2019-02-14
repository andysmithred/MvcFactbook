using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BranchFlagView : View<BranchFlag>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Branch Id")]
        public int BranchId => ViewObject.BranchId;

        [Required]
        [Display(Name = "Flag Id")]
        public int FlagId => ViewObject.FlagId;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? Start => ViewObject.Start;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? End => ViewObject.End;

        #endregion Database Properties

        #region Foreign Properties

        public BranchView Branch => GetView<BranchView, Branch>(ViewObject.Branch);
        public FlagView Flag => GetView<FlagView, Flag>(ViewObject.Flag);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Branch.Name + ":" + Flag.Name;

        #endregion Other Properties
    }
}
