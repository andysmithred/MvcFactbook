using MvcFactbook.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipClassView : View<ShipClass>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        public string SubClassName => ViewObject.SubClassName;

        public int? Displacement => ViewObject.Displacement;

        public double? Length => ViewObject.Length;

        public double? Beam => ViewObject.Beam;

        public int? Propulsion => ViewObject.Propulsion;

        public double? Speed => ViewObject.Speed;

        public int? Crew => ViewObject.Crew;

        #endregion Database Properties

        #region Foreign Properties

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + SubClassNameLabel;

        public string SubClassNameLabel => String.IsNullOrEmpty(SubClassName) ? "--" : SubClassName;

        #endregion Other Properties
    }
}
