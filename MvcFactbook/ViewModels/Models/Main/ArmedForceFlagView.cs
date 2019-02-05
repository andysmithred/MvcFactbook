using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ArmedForceFlagView : View<ArmedForceFlag>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public int ArmedForceId => ViewObject.ArmedForceId;

        [Required]
        public int FlagId => ViewObject.FlagId;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? Start => ViewObject.Start;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? End => ViewObject.End;

        #endregion Database Properties

        #region Foreign Properties

        public ArmedForceView ArmedForce => GetView<ArmedForceView, ArmedForce>(ViewObject.ArmedForce);
        public FlagView Flag => GetView<FlagView, Flag>(ViewObject.Flag);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => ArmedForce.Name + ":" + Flag.Name;

        #endregion Other Properties
    }
}
