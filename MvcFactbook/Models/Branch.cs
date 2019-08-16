using MvcFactbook.Code.Classes;
using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Branch: IComplete
    {
        #region Constructor

        public Branch()
        {
            BranchFlags = new HashSet<BranchFlag>();
            ShipServices = new HashSet<ShipService>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Armed Force")]
        public int ArmedForceId { get; set; }

        [Required]
        [Display(Name = "Branch Type")]
        public int BranchTypeId { get; set; }

        [Required]
        [Display(Name = "Emblem")]
        public bool HasEmblem { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Armed Force")]
        public ArmedForce ArmedForce { get; set; }

        [Display(Name = "Branch Type")]
        public BranchType BranchType { get; set; }

        public ICollection<BranchFlag> BranchFlags { get; set; }

        public ICollection<ShipService> ShipServices { get; set; }

        #endregion Foreign Properties
    }
}