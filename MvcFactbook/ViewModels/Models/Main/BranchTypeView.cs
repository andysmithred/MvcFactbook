﻿using MvcFactbook.Models;
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

        //public ICollection<FlagView> Flags => ArmedForceFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        #endregion Other Properties
    }
}
