using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class DockyardView : View<Dockyard>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Name")]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Complete")]
        public bool Complete => ViewObject.Complete;

        [Display(Name = "Short Name")]
        public string ShortName => ViewObject.ShortName;

        [Display(Name = "Alternative Name")]
        public string AlternativeNames => ViewObject.AlternativeNames;

        [Display(Name = "Location")]
        public string Location => ViewObject.Location;

        [Display(Name = "Latitude")]
        public double? Latitude => ViewObject.Latitude;

        [Display(Name = "Longitude")]
        public double? Longitude => ViewObject.Longitude;

        [Display(Name = "Zoom")]
        public int? Zoom => ViewObject.Zoom;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipView> Ships => GetViewList<ShipView, Ship>(ViewObject.Ships);

        public ICollection<PoliticalEntityDockyardView> PoliticalEntityDockyards => GetViewList<PoliticalEntityDockyardView, PoliticalEntityDockyard>(ViewObject.PoliticalEntityDockyards);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name;

        public ICollection<PoliticalEntityView> PoliticalEntities => PoliticalEntityDockyards.Select(f => f.PoliticalEntity).Distinct(f => f.Id).ToList();

        public bool HasFlag => PoliticalEntities.Count > 0;

        public FlagView Flag => PoliticalEntities.OrderByDescending(x => x.AbsoluteEnd).FirstOrDefault()?.CurrentFlag;

        public string ImageSource => Flag?.ImageSource;

        public string CompleteIcon => CommonFunctions.GetCompleteIcon(Complete);

        #endregion Other Properties

        #region Methods

        public PoliticalEntityView GetPoliticalEntityForDate(DateTime date)
        {
            if (PoliticalEntities.Count > 0)
            {
                foreach (var politicalEntity in PoliticalEntities)
                {
                    foreach (var era in politicalEntity.PoliticalEntityEras)
                    {
                        if (era.AbsoluteStart < date && date <= era.AbsoluteEnd)
                        {
                            return politicalEntity;
                        }
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public FlagView GetFlagForDate(DateTime date)
        {
            var politicalEntity = GetPoliticalEntityForDate(date);

            if (politicalEntity != null)
            {
                return politicalEntity.GetFlagForDate(date);
            }
            else
            {
                return null;
            }
        }

        #endregion Methods

    }
}
