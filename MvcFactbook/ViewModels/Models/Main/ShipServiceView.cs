using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipServiceView : View<ShipService>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(10)]
        public string Penant => ViewObject.Penant;

        [Required]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Ship Id")]
        public int ShipId => ViewObject.ShipId;

        [Required]
        [Display(Name = "Ship Class Id")]
        public int ShipClassId => ViewObject.ShipClassId;

        [Required]
        [Display(Name = "Ship Sub-Type Id")]
        public int ShipSubTypeId => ViewObject.ShipSubTypeId;

        [DataType(DataType.Date)]
        [Display(Name = "Start Service")]
        public DateTime? StartService => ViewObject.StartService;

        [DataType(DataType.Date)]
        [Display(Name = "End Service")]
        public DateTime? EndService => ViewObject.EndService;

        [Display(Name = "Branch Id")]
        public int? BranchId => ViewObject.BranchId;

        public string Fate => ViewObject.Fate;

        [Required]
        public bool Active => ViewObject.Active;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Ship")]
        public ShipView Ship => GetView<ShipView, Ship>(ViewObject.Ship);

        [Display(Name = "Ship Class")]
        public ShipClassView ShipClass => GetView<ShipClassView, ShipClass>(ViewObject.ShipClass);

        [Display(Name = "Ship Sub-Type")]
        public ShipSubTypeView ShipSubType => GetView<ShipSubTypeView, ShipSubType>(ViewObject.ShipSubType);

        [Display(Name = "Branch")]
        public BranchView Branch => GetView<BranchView, Branch>(ViewObject.Branch);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Penant;

        public DateTime AbsoluteStartService => StartService.HasValue ? StartService.Value : DateTime.MinValue;

        public DateTime AbsoluteEndService => EndService.HasValue ? EndService.Value : DateTime.MaxValue;

        public ICollection<BranchFlagView> BranchFlags => GetBranchFlags(AbsoluteStartService, AbsoluteEndService);

        public bool HasFlag => BranchFlags.Count > 0;

        public BranchFlagView CurrentBranchFlag => BranchFlags.OrderBy(x => x.AbsoluteStart).ToList().FirstOrDefault();

        public FlagView CurrentFlag => CurrentBranchFlag?.Flag;

        public string ImageSource => CurrentFlag?.ImageSource;

        public string Image => CurrentFlag.Image;

        public string StartServiceLabel => CommonFunctions.GetDateLabel(StartService);

        public string EndServiceLabel => CommonFunctions.GetDateLabel(EndService);

        #endregion Other Properties

        #region Methods


        private ICollection<BranchFlagView> GetBranchFlags(DateTime startDate, DateTime endDate)
        {
            ICollection<BranchFlagView> result = new List<BranchFlagView>();

            if(Branch != null)
            {
                foreach (var item in Branch.BranchFlags)
                {
                    if(IsValidBranchFlag(item, startDate,endDate))
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        private bool IsValidBranchFlag(BranchFlagView branchFlag, DateTime startDate, DateTime endDate)
        {
            if(branchFlag.AbsoluteEnd < startDate)
            {
                return false;
            }
            if(branchFlag.AbsoluteStart > endDate)
            {
                return false;
            }

            return true;
        }

        #endregion Methods
    }
}