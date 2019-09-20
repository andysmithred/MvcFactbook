using MvcFactbook.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class PoliticalEntity: IComplete
    {
        #region Constructor

        public PoliticalEntity()
        {
            PoliticalEntityFlags = new HashSet<PoliticalEntityFlag>();
            PoliticalEntityDockyards = new HashSet<PoliticalEntityDockyard>();
            PoliticalEntityEras = new HashSet<PoliticalEntityEra>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(3)]
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Exists")]
        public bool Exists { get; set; }
        
        [Required]
        [Display(Name = "Emblem")]
        public bool HasEmblem { get; set; }

        [Required]
        [Display(Name = "Political Entity Type Id")]
        public int PoliticalEntityTypeId { get; set; }

        [Required]
        [Display(Name = "Complete")]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity Type")]
        public PoliticalEntityType PoliticalEntityType { get; set; }

        public ICollection<PoliticalEntityDockyard> PoliticalEntityDockyards { get; set; }

        public ICollection<PoliticalEntityFlag> PoliticalEntityFlags { get; set; }

        public ICollection<PoliticalEntitySucceeding> PrecedingEntities { get; set; }

        public ICollection<PoliticalEntitySucceeding> SucceedingEntities { get; set; }

        public ICollection<PoliticalEntityEra> PoliticalEntityEras { get; set; }

        #endregion Foreign Properties
    }
}