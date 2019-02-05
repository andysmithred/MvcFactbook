using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BranchView : View<Branch>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Armed Force")]
        public int ArmedForceId => ViewObject.ArmedForceId;

        [Required]
        [Display(Name = "Branch Type")]
        public int BranchTypeId => ViewObject.BranchTypeId;

        [Required]
        [Display(Name = "Emblem")]
        public bool HasEmblem => ViewObject.HasEmblem;

        #endregion Database Properties

        #region Foreign Properties

        public BranchTypeView BranchType => GetView<BranchTypeView, BranchType>(ViewObject.BranchType);

        public ArmedForceView ArmedForce => GetView<ArmedForceView, ArmedForce>(ViewObject.ArmedForce);

        public ICollection<BranchFlagView> BranchFlags => GetViewList<BranchFlagView, BranchFlag>(ViewObject.BranchFlags);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + ArmedForce?.Name;

        public bool HasFlag => false;

        public ICollection<FlagView> Flags => BranchFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}