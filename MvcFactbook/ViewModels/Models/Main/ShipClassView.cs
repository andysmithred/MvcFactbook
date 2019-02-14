using MvcFactbook.Models;
using System;
using System.Collections.Generic;
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

        [Display(Name = "Sub-class Name")]
        public string SubClassName => ViewObject.SubClassName;

        public int? Displacement => ViewObject.Displacement;

        public double? Length => ViewObject.Length;

        public double? Beam => ViewObject.Beam;

        public int? Propulsion => ViewObject.Propulsion;

        public double? Speed => ViewObject.Speed;

        public int? Crew => ViewObject.Crew;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + SubClassNameLabel;

        public string SubClassNameLabel => String.IsNullOrEmpty(SubClassName) ? "--" : SubClassName;

        public string DisplacementLabel => Displacement.HasValue ? Displacement.Value.ToString("N0") : "--";

        public string LengthLabel => Length.HasValue ? Length.Value.ToString("N0") : "--";

        public string BeamLabel => Beam.HasValue ? Beam.Value.ToString("N0") : "--";

        public string PropulsionLabel => Propulsion.HasValue ? Propulsion.Value.ToString("N0") : "--";

        public string SpeedLabel => Speed.HasValue ? Speed.Value.ToString("N0") : "--";

        public string CrewLabel => Crew.HasValue ? Crew.Value.ToString("N0") : "--";

        #endregion Other Properties
    }
}