using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BuilderView : View<Builder>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        public int? Founded => ViewObject.Founded;

        public int? Defunct => ViewObject.Defunct;

        [Required]
        public bool Complete => ViewObject.Complete;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipView> Ships => GetViewList<ShipView, Ship>(ViewObject.Ships);

        public ICollection<PoliticalEntityBuilderView> PoliticalEntityBuilders => GetViewList<PoliticalEntityBuilderView, PoliticalEntityBuilder>(ViewObject.PoliticalEntityBuilders);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name;

        public string FoundedLabel => Founded.HasValue ? Founded.Value.ToString() : "--";

        public string DefunctLabel => Defunct.HasValue ? Defunct.Value.ToString() : "--";

        public ICollection<PoliticalEntityView> PoliticalEntities => PoliticalEntityBuilders.Select(f => f.PoliticalEntity).Distinct(f => f.Id).ToList();

        public bool HasFlag => PoliticalEntities.Count > 0;

        public FlagView Flag => PoliticalEntities.OrderByDescending(x => x.StartDate).FirstOrDefault()?.CurrentFlag;

        public string ImageSource => Flag?.ImageSource;

        public string CompleteIcon => CommonFunctions.GetCompleteIcon(Complete);

        #endregion Other Properties

        #region Methods

        public PoliticalEntityView GetPoliticalEntityForDate(DateTime date)
        {
            if(PoliticalEntities.Count > 0)
            {
                foreach (var politicalEntity in PoliticalEntities)
                {
                    foreach (var era in politicalEntity.PoliticalEntityEras)
                    {
                        if(era.AbsoluteStart < date && date <= era.AbsoluteEnd)
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

            if(politicalEntity != null)
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
