using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class BranchView : View<Branch>
    {
        #region Private Variables

        private Fleet totalFleet = null;
        private Fleet activeFleet = null;
        private Fleet inactiveFleet = null;

        #endregion Private Variables

        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(50)]
        [Required]
        public string Name => ViewObject.Name;

        [Required]
        [Display(Name = "Armed Force")]
        public int ArmedForceId => ViewObject.ArmedForceId;

        [Required]
        [Display(Name = "Branch Type")]
        public int BranchTypeId => ViewObject.BranchTypeId;

        [Required]
        [Display(Name = "Emblem")]
        public bool HasEmblem => ViewObject.HasEmblem;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Branch Type")]
        public BranchTypeView BranchType => GetView<BranchTypeView, BranchType>(ViewObject.BranchType);

        [Display(Name = "Armed Force")]
        public ArmedForceView ArmedForce => GetView<ArmedForceView, ArmedForce>(ViewObject.ArmedForce);

        public ICollection<BranchFlagView> BranchFlags => GetViewList<BranchFlagView, BranchFlag>(ViewObject.BranchFlags);

        public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + ArmedForce?.Name;

        public bool HasFlag => Flags.Count > 0;

        public ICollection<FlagView> Flags => BranchFlags.Select(f => f.Flag).Distinct(f => f.Id).ToList();

        public FlagView CurrentFlag => Flags.OrderByDescending(x => x.StartDate).FirstOrDefault();

        public string ImageSource => CurrentFlag?.ImageSource;

        public string Image => CurrentFlag?.Image;




        public ICollection<ShipServiceView> ActiveServices => ShipServices.Where(x => x.Active).ToList();

        public ICollection<ShipServiceView> InactiveServices => ShipServices.Where(x => !x.Active).ToList();

        public bool HasFleet => ShipServices.Count > 0;

        public Fleet TotalFleet => totalFleet ?? (totalFleet = new Fleet(ShipServices));

        public Fleet ActiveFleet => activeFleet ?? (activeFleet = new Fleet(ActiveServices));

        public Fleet InactiveFleet => inactiveFleet ?? (inactiveFleet = new Fleet(InactiveServices));

        #endregion Other Properties

        #region Methods

        public FlagView GetFlagByDate(DateTime date)
        {
            if(BranchFlags.Count > 0)
            {
                foreach (var item in BranchFlags)
                {
                    if(item.AbsoluteStart <= date && date <= item.AbsoluteEnd)
                    {
                        return item.Flag;
                    }
                }

                //If we get here return null
                return null;
            }
            else
            {
                return null;
            }
        }

        #endregion Methods
    }
}