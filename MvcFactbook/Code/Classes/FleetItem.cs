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

        private IEnumerable<ShipServiceView> shipServicesList = null;
        private IEnumerable<ShipView> shipsList = null;

        #endregion Private Declarations

        #region Constructors

        public FleetItem(string name, IEnumerable<ShipServiceView> shipServices)
        {
            Name = name;
            ShipServicesList = shipServices;
        }

        public FleetItem(string name, IEnumerable<ShipView> ships)
        {
            Name = name;
            ShipsList = ships;
        }

        #endregion Conbstructors

        #region Public Properties

        public IEnumerable<ShipServiceView> ShipServicesList
        {
            get => shipServicesList ?? (shipServicesList = ShipsList.SelectMany(x => x.ShipServices).Distinct(x => x.Id));
            set => shipServicesList = value;
        }

        public IEnumerable<ShipView> ShipsList
        {
            get => shipsList ?? (shipsList = ShipServicesList.Select(x => x.Ship).Distinct(x => x.Id));
            set => shipsList = value;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int ShipServices => ShipServicesList.Count();

        public int Tonnage => ShipServicesList.Sum(x => x.ShipClass.Displacement.HasValue ? x.ShipClass.Displacement.Value : 0);
        public string TonnageLabel => DisplacementCount > 0 ? Tonnage.ToString("N0") + " tons" : "--";

        public int DisplacementCount => ShipServicesList.Sum(x => x.ShipClass.Displacement.HasValue ? 1: 0);
        public double DisplacementAverage => CommonFunctions.GetAverage(Tonnage, DisplacementCount);
        public string DisplacementAverageLabel => DisplacementCount > 0 ? DisplacementAverage.ToString("N0") + " tons" : "--";

        public double LengthTotal => ShipServicesList.Sum(x => x.ShipClass.Length.HasValue ? x.ShipClass.Length.Value : 0);
        public int LengthCount => ShipServicesList.Sum(x => x.ShipClass.Length.HasValue ? 1 : 0);
        public double LengthAverage => CommonFunctions.GetAverage(LengthTotal, LengthCount);
        public string LengthAverageLabel => LengthCount > 0 ? LengthAverage.ToString("N0") + " m" : "--";

        public double BeamTotal => ShipServicesList.Sum(x => x.ShipClass.Beam.HasValue ? x.ShipClass.Beam.Value : 0);
        public int BeamCount => ShipServicesList.Sum(x => x.ShipClass.Beam.HasValue ? 1 : 0);
        public double BeamAverage => CommonFunctions.GetAverage(BeamTotal, BeamCount);
        public string BeamAverageLabel => BeamCount > 0 ? BeamAverage.ToString("N0") + " m" : "--";

        #endregion Public Properties

    }
}
