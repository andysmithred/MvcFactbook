﻿using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MvcFactbook.Code.Classes;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipClassView : View<ShipClass>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        [Display(Name = "Sub-class")]
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

        public ICollection<SucceedingClassView> PrecedingClasses => GetViewList<SucceedingClassView, SucceedingClass>(ViewObject.PrecedingClasses);

        public ICollection<SucceedingClassView> SucceedingClasses => GetViewList<SucceedingClassView, SucceedingClass>(ViewObject.SucceedingClasses);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + SubClassNameLabel;

        public string FullName => Name + " (" + SubClassNameLabel + ")";

        public string SubClassNameLabel => String.IsNullOrEmpty(SubClassName) ? "--" : SubClassName;

        public string DisplacementLabel => Displacement.HasValue ? Displacement.Value.ToString("N0") + " tons" : "--";

        public string LengthLabel => Length.HasValue ? Length.Value.ToString("N0") + " m" : "--";

        public string BeamLabel => Beam.HasValue ? Beam.Value.ToString("N0") + " m" : "--";

        public string PropulsionLabel => Propulsion.HasValue ? Propulsion.Value.ToString("N0") + " hp" : "--";

        public string SpeedLabel => Speed.HasValue ? Speed.Value.ToString("N0") + " knots" : "--";

        public string CrewLabel => Crew.HasValue ? Crew.Value.ToString("N0") : "--";

        public ICollection<ShipClassView> PrecedingShipClasses => PrecedingClasses.Select(x => x.PrecedingShipClass).Distinct(x => x.Id).ToList();

        public ICollection<ShipClassView> SucceedingShipClasses => SucceedingClasses.Select(x => x.SucceedingShipClass).Distinct(x => x.Id).ToList();

        #endregion Other Properties
    }
}