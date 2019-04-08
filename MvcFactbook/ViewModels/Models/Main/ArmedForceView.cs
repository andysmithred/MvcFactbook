using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ArmedForceView : View<ArmedForce>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Name => ViewObject.Name;

        [StringLength(5)]
        [Required]
        public string Code => ViewObject.Code;

        [Required]
        public bool IsActive => ViewObject.IsActive;

        public long? Budget => ViewObject.Budget;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ArmedForceFlagView> ArmedForceFlags => GetViewList<ArmedForceFlagView, ArmedForceFlag>(ViewObject.ArmedForceFlags);

        public ICollection<BranchView> Branches => GetViewList<BranchView, Branch>(ViewObject.Branches);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Code;

        public ICollection<FlagView> Flags => ArmedForceFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        public bool HasFlag => Flags.Count > 0;

        public FlagView Flag => Flags.OrderByDescending(x => x.StartDate).FirstOrDefault();

        public string ImageSource => Flag?.ImageSource;

        public string BudgetLabel => Budget.HasValue ? "$" + Budget.Value.ToString("N0") : "--";

        #endregion Other Properties

        #region Methods

        #endregion Methods

    }
}
