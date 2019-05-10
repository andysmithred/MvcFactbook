using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MvcFactbook.Code.Classes;
using System.Text;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipClassView : View<ShipClass>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        [Display(Name = "Sub-Class")]
        public string SubClassName => ViewObject.SubClassName;

        public int? Displacement => ViewObject.Displacement;

        public double? Length => ViewObject.Length;

        public double? Beam => ViewObject.Beam;

        public int? Propulsion => ViewObject.Propulsion;

        public double? Speed => ViewObject.Speed;

        public int? Crew => ViewObject.Crew;

        public int? Year => ViewObject.Year;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        public ICollection<SucceedingClassView> PrecedingClasses => GetViewList<SucceedingClassView, SucceedingClass>(ViewObject.PrecedingClasses);

        public ICollection<SucceedingClassView> SucceedingClasses => GetViewList<SucceedingClassView, SucceedingClass>(ViewObject.SucceedingClasses);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => GetListName();

        public string FullName => Name + (String.IsNullOrEmpty(SubClassName) ? string.Empty : ":" + SubClassName);

        public string SubClassNameLabel => String.IsNullOrEmpty(SubClassName) ? "--" : SubClassName;

        public string DisplacementLabel => Displacement.HasValue ? Displacement.Value.ToString("N0") + " tons" : "--";

        public string LengthLabel => Length.HasValue ? Length.Value.ToString("N0") + " m" : "--";

        public string BeamLabel => Beam.HasValue ? Beam.Value.ToString("N0") + " m" : "--";

        public string PropulsionLabel => Propulsion.HasValue ? Propulsion.Value.ToString("N0") + " hp" : "--";

        public string SpeedLabel => Speed.HasValue ? Speed.Value.ToString("N0") + " knots" : "--";

        public string CrewLabel => Crew.HasValue ? Crew.Value.ToString("N0") : "--";

        public string YearLabel => Year.HasValue ? Year.Value.ToString() : "--";

        public ICollection<ShipClassView> PrecedingShipClasses => PrecedingClasses.Select(x => x.PrecedingShipClass).Distinct(x => x.Id).ToList();

        public ICollection<ShipClassView> SucceedingShipClasses => SucceedingClasses.Select(x => x.SucceedingShipClass).Distinct(x => x.Id).ToList();

        public ICollection<ShipView> Ships => ShipServices.Select(x => x.Ship).Distinct(x => x.Id).ToList();

        public ICollection<BranchView> Branches => ShipServices.Select(x => x.Branch).Distinct(x => x.Id).ToList();

        public ICollection<ShipSubTypeView> ShipSubTypes => ShipServices.Select(x => x.ShipSubType).Distinct(x => x.Id).ToList();

        public DateTime? StartService => ShipServices.OrderBy(x => x.StartService)?.FirstOrDefault()?.StartService;

        public DateTime? EndService => GetEndService(ShipServices);

        [Display(Name = "In Commission")]
        public string InCommission => CommonFunctions.GetYears(StartService, EndService);

        [Display(Name = "Years Service")]
        public TimeSpan? TimeSpan => CommonFunctions.GetTimepan(StartService, EndService);

        public string TimeSpanLabel => CommonFunctions.Format(TimeSpan);

        public bool IsActive => ShipServices.Where(x => x.Active).Count() > 0;

        #endregion Other Properties

        #region Methods

        private string GetListName()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Name);

            //Add the SubClassName (if available)
            if(!string.IsNullOrEmpty(SubClassName))
            {
                sb.Append(":");
                sb.Append(SubClassName);
            }

            //Add the year (if available)
            if(Year.HasValue)
            {
                sb.Append(":");
                sb.Append(Year.Value.ToString());
            }

            return sb.ToString();
        }

        private DateTime? GetEndService(ICollection<ShipServiceView> shipServices)
        {
            if(IsActive)
            {
                return null;
            }
            else
            {
                return ShipServices.OrderByDescending(x => x.EndService)?.FirstOrDefault()?.EndService;
            }
        }

        #endregion Methods

    }
}