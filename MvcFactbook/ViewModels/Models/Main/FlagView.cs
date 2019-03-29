using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class FlagView : View<Flag>
    {
        #region Private Declarations

        private const string FLAG_PATH = "/images/flags/";
        private const string FLAG_EXTENSION = ".png";

        #endregion Private Declarations

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
        [DataType(DataType.Date)]
        public DateTime? StartDate => ViewObject.StartDate;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate => ViewObject.EndDate;

        [Required]
        public bool Active => ViewObject.Active;

        #endregion Database Properties

        #region Foreign Properties
        
        public ICollection<ArmedForceFlagView> ArmedForceFlags => GetViewList<ArmedForceFlagView, ArmedForceFlag>(ViewObject.ArmedForceFlags);

        public ICollection<BranchFlagView> BranchFlags => GetViewList<BranchFlagView, BranchFlag>(ViewObject.BranchFlags);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + " : " + Code;

        public string Image => Code + FLAG_EXTENSION;

        public string ImageSource => FLAG_PATH + Image;

        public string StartDateLabel => CommonFunctions.GetDateLabel(StartDate);

        public string EndDateLabel => CommonFunctions.GetDateLabel(EndDate);

        public string DateLabel => CommonFunctions.GetDateLabel(StartDate, EndDate);

        public ICollection<ArmedForceView> ArmedForces => ArmedForceFlags.Select(f => f.ArmedForce).Distinct(f => f.Id).ToList();

        public ICollection<BranchView> Branches => BranchFlags.Select(f => f.Branch).Distinct(f => f.Id).ToList();

        #endregion Other Properties

    }
}
