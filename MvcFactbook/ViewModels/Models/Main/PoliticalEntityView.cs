﻿using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityView : View<PoliticalEntity>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        [Display(Name = "Short Name")]
        public string ShortName => ViewObject.ShortName;

        [Required]
        public string Name => ViewObject.Name;

        [Display(Name = "Full Name")]
        public string FullName => ViewObject.FullName;

        [StringLength(3)]
        [Required]
        public string Code => ViewObject.Code;

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate => ViewObject.StartDate;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate => ViewObject.EndDate;

        [Required]
        public bool Exists => ViewObject.Exists;

        [Display(Name = "Emblem")]
        [Required]
        public bool HasEmblem => ViewObject.HasEmblem;

        [Display(Name = "Political Entity Type Id")]
        [Required]
        public int PoliticalEntityTypeId => ViewObject.PoliticalEntityTypeId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity Type")]
        public PoliticalEntityTypeView PoliticalEntityType => GetView<PoliticalEntityTypeView, PoliticalEntityType>(ViewObject.PoliticalEntityType);

        public ICollection<PoliticalEntityFlagView> PoliticalEntityFlags => GetViewList<PoliticalEntityFlagView, PoliticalEntityFlag>(ViewObject.PoliticalEntityFlags);

        [Display(Name = "Preceding Entities")]
        public ICollection<PoliticalEntitySucceedingView> PrecedingEntities => GetViewList<PoliticalEntitySucceedingView, PoliticalEntitySucceeding>(ViewObject.PrecedingEntities);

        [Display(Name = "Succeeding Entities")]
        public ICollection<PoliticalEntitySucceedingView> SucceedingEntities => GetViewList<PoliticalEntitySucceedingView, PoliticalEntitySucceeding>(ViewObject.SucceedingEntities);

        #endregion Foreign Properties

        #region Other Properties

        public string CombinedName => ShortName + ": " + Name;

        public override string ListName => ShortName + " " + Years;

        public string Years => GetYears();

        public string StartDateLabel => CommonFunctions.GetDateLabel(StartDate);

        public string EndDateLabel => CommonFunctions.GetDateLabel(EndDate);

        [Display(Name = "Time Span")]
        public TimeSpan? TimeSpan => CommonFunctions.GetTimepan(StartDate, EndDate);

        public string TimeSpanLabel => CommonFunctions.Format(TimeSpan);

        public ICollection<FlagView> Flags => PoliticalEntityFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        public bool HasFlag => Flags.Count > 0;

        public FlagView CurrentFlag => Flags.OrderByDescending(x => x.StartDate).FirstOrDefault();

        public string ImageSource => CurrentFlag?.ImageSource;

        public string Image => CurrentFlag?.Image;

        public ICollection<PoliticalEntityView> PrecedingPoliticalEntities => PrecedingEntities.Select(x => x.PrecedingEntity).Distinct(x => x.Id).ToList();

        public ICollection<PoliticalEntityView> SucceedingPoliticalEntities => SucceedingEntities.Select(x => x.SucceedingEntity).Distinct(x => x.Id).ToList();

        #endregion Other Properties

        #region Methods

        private string GetYears()
        {
            StringBuilder s = new StringBuilder();

            if (StartDate.HasValue || EndDate.HasValue)
            {
                s.Append(" [");

                if (StartDate.HasValue)
                {
                    s.Append(StartDate.Value.Year.ToString());
                }
                else
                {
                    s.Append("?");
                }

                s.Append(" - ");

                if (EndDate.HasValue)
                {
                    s.Append(EndDate.Value.Year.ToString());
                }
                else
                {
                    if (Exists)
                    {
                        s.Append("now");
                    }
                    else
                    {
                        s.Append("?");
                    }
                }

                s.Append("]");
            }
            else
            {
                s.Append(" [--]");
            }

            return s.ToString();
        }

        #endregion Methods
    }
}
