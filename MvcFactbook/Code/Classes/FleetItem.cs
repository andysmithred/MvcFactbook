using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcFactbook.Code.Classes;

namespace MvcFactbook.Code.Classes
{
    public class FleetItem
    {
        #region Private Declarations

        private IEnumerable<ShipServiceView> servicesList = null;
        private IEnumerable<ShipView> shipsList = null;

        #endregion Private Declarations

        #region Constructors

        public FleetItem(string name, IEnumerable<ShipServiceView> services)
        {
            Name = name;
            ServicesList = services;
        }

        public FleetItem(string name, IEnumerable<ShipView> ships)
        {
            Name = name;
            ShipsList = ships;
        }

        #endregion Conbstructors

        #region Public Properties

        public IEnumerable<ShipServiceView> ServicesList
        {
            get => servicesList ?? (servicesList = ShipsList.SelectMany(x => x.ShipServices).Distinct(x => x.Id));
            set => servicesList = value;
        }

        public IEnumerable<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = ServicesList.Select(x => x.Ship).Distinct(x => x.Id));
            set => shipsList = value;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int Services => ServicesList.Count();

        public int Tonnage => ServicesList.Sum(x => x.ShipClass.Displacement.HasValue ? x.ShipClass.Displacement.Value : 0);
        public string TonnageLabel => DisplacementCount > 0 ? Tonnage.ToString("N0") + " tons" : "--";

        public int DisplacementCount => ServicesList.Sum(x => x.ShipClass.Displacement.HasValue ? 1: 0);
        public double DisplacementAverage => CommonFunctions.GetAverage(Tonnage, DisplacementCount);
        public string DisplacementAverageLabel => DisplacementCount > 0 ? DisplacementAverage.ToString("N0") + " tons" : "--";

        public double LengthTotal => ServicesList.Sum(x => x.ShipClass.Length.HasValue ? x.ShipClass.Length.Value : 0);
        public int LengthCount => ServicesList.Sum(x => x.ShipClass.Length.HasValue ? 1 : 0);
        public double LengthAverage => CommonFunctions.GetAverage(LengthTotal, LengthCount);
        public string LengthAverageLabel => LengthCount > 0 ? LengthAverage.ToString("N0") + " m" : "--";

        public double BeamTotal => ServicesList.Sum(x => x.ShipClass.Beam.HasValue ? x.ShipClass.Beam.Value : 0);
        public int BeamCount => ServicesList.Sum(x => x.ShipClass.Beam.HasValue ? 1 : 0);
        public double BeamAverage => CommonFunctions.GetAverage(BeamTotal, BeamCount);
        public string BeamAverageLabel => BeamCount > 0 ? BeamAverage.ToString("N0") + " m" : "--";

        #endregion Public Properties

    }
}
