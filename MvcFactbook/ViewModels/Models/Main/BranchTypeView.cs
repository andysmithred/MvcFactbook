using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BranchTypeView : View<BranchType>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Type => ViewObject.Type;

        [StringLength(5)]
        [Required]
        public string Code => ViewObject.Code;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<BranchView> Branches => GetViewList<BranchView, Branch>(ViewObject.Branches);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type + ":" + Code;

        public string IconLight => Code + "-light";

        public string IconDark => Code + "-dark";

        public string IconLightPath => CommonFunctions.GetIconFullPath(IconLight);

        public string IconDarkPath => CommonFunctions.GetIconFullPath(IconDark);

        #endregion Other Properties
    }
}
