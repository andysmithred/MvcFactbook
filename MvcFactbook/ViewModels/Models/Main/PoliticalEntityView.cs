using MvcFactbook.Code.Classes;
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

        [Display(Name = "Start Date OLD")]
        [DataType(DataType.Date)]
        public DateTime? StartDateOLD => ViewObject.StartDate;

        [Display(Name = "End Date OLD")]
        [DataType(DataType.Date)]
        public DateTime? EndDateOLD => ViewObject.EndDate;

        [Required]
        public bool Exists => ViewObject.Exists;

        [Display(Name = "Emblem")]
        [Required]
        public bool HasEmblem => ViewObject.HasEmblem;

        [Display(Name = "Political Entity Type Id")]
        [Required]
        public int PoliticalEntityTypeId => ViewObject.PoliticalEntityTypeId;

        [Required]
        public bool Complete => ViewObject.Complete;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity Type")]
        public PoliticalEntityTypeView PoliticalEntityType => GetView<PoliticalEntityTypeView, PoliticalEntityType>(ViewObject.PoliticalEntityType);

        public ICollection<PoliticalEntityFlagView> PoliticalEntityFlags => GetViewList<PoliticalEntityFlagView, PoliticalEntityFlag>(ViewObject.PoliticalEntityFlags);

        public ICollection<PoliticalEntityDockyardView> PoliticalEntityDockyards => GetViewList<PoliticalEntityDockyardView, PoliticalEntityDockyard>(ViewObject.PoliticalEntityDockyards);

        [Display(Name = "Preceding Entities")]
        public ICollection<PoliticalEntitySucceedingView> PrecedingEntities => GetViewList<PoliticalEntitySucceedingView, PoliticalEntitySucceeding>(ViewObject.PrecedingEntities);

        [Display(Name = "Succeeding Entities")]
        public ICollection<PoliticalEntitySucceedingView> SucceedingEntities => GetViewList<PoliticalEntitySucceedingView, PoliticalEntitySucceeding>(ViewObject.SucceedingEntities);

        [Display(Name = "Political Entity Era")]
        public ICollection<PoliticalEntityEraView> PoliticalEntityEras => GetViewList<PoliticalEntityEraView, PoliticalEntityEra>(ViewObject.PoliticalEntityEras);

        #endregion Foreign Properties

        #region Other Properties

        public string CombinedName => ShortName + ": " + Name;

        public override string ListName => ShortName + " " + Years;

        public DateTime? StartDate => PoliticalEntityEras.OrderBy(x => x.StartDate).FirstOrDefault()?.StartDate;

        public DateTime? EndDate => PoliticalEntityEras.OrderByDescending(x => x.StartDate).FirstOrDefault()?.EndDate;

        public string Years => GetYears();

        public string StartDateLabel => CommonFunctions.GetDateLabel(StartDate);

        public string EndDateLabel => CommonFunctions.GetDateLabel(EndDate);

        public DateTime AbsoluteStart => StartDate.HasValue ? StartDate.Value : DateTime.MinValue;

        public DateTime AbsoluteEnd => EndDate.HasValue ? EndDate.Value : DateTime.MaxValue;

        [Display(Name = "Time Span")]
        public TimeSpan? TimeSpan => new TimeSpan(PoliticalEntityEras.Sum(x => x.TimeSpan.Value.Ticks));

        public string TimeSpanLabel => CommonFunctions.Format(TimeSpan);

        public ICollection<FlagView> Flags => PoliticalEntityFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        public bool HasFlag => Flags.Count > 0;

        //TODO:AS remove
        public FlagView CurrentFlag => Flags.OrderByDescending(x => x.StartDate).FirstOrDefault();

        public FlagView LastFlag => EndDate.HasValue ? GetFlagForDate(EndDate.Value) : GetFlagForDate(DateTime.Now);


        public string ImageSourceCurrent => CurrentFlag?.ImageSource;
        public string ImageSource => LastFlag?.ImageSource;

        public string Image => CurrentFlag?.Image;

        public FlagView FirstFlag => Flags.OrderBy(x => x.StartDate).FirstOrDefault();

        public string FirstFlagImageSource => FirstFlag?.ImageSource;

        public string FirstFlagImage => CurrentFlag?.Image;

        public ICollection<PoliticalEntityView> PrecedingPoliticalEntities => PrecedingEntities.Select(x => x.PrecedingEntity).Distinct(x => x.Id).ToList();

        public ICollection<PoliticalEntityView> SucceedingPoliticalEntities => SucceedingEntities.Select(x => x.SucceedingEntity).Distinct(x => x.Id).ToList();

        public ICollection<DockyardView> Dockyards => PoliticalEntityDockyards.Select(f => f.Dockyard).Distinct(f => f.Id).ToList();

        #endregion Other Properties

        #region Methods

        private string GetYears()
        {
            StringBuilder sb = new StringBuilder();
            bool firstTime = true;

            if(PoliticalEntityEras.Count > 0)
            {
                sb.Append(" [");

                foreach (var item in PoliticalEntityEras)
                {
                    if(!firstTime)
                    {
                        sb.Append(":");
                    }

                    sb.Append(item.Years);

                    firstTime = false;
                }

                sb.Append("]");
            }
            else
            {
                sb.Append(" [--]");
            }

            return sb.ToString();
        }

        public FlagView GetFlagForDate(DateTime date)
        {
            Console.WriteLine("#######");
            Console.WriteLine("IN --> GetFlagForDate: " + date.ToString());
            Console.WriteLine("Name: " + CombinedName);
            Console.WriteLine("StartDate: " + StartDate);
            Console.WriteLine("EndDate: " + EndDate);
            Console.WriteLine("PoliticalEntityEras: " + PoliticalEntityEras.Count);


            if (PoliticalEntityFlags.Count > 0)
            {
                foreach (var item in PoliticalEntityFlags)
                {
                    if(item.AbsoluteStart < date && date <= item.AbsoluteEnd)
                    {
                        return item.Flag;
                    }
                }

                Console.WriteLine("RETURNING --> NULL 1: " + date.ToString());
                return null;
            }
            else
            {
                Console.WriteLine("RETURNING  --> Null 2: " + date.ToString());
                return null;
            }
        }

        #endregion Methods
    }
}
