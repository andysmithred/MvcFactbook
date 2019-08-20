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
            PoliticalEntityBuilders = new HashSet<PoliticalEntityBuilder>();
            PoliticalEntityEras = new HashSet<PoliticalEntityEra>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Display(Name = "Short Name")]
        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(3)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool Exists { get; set; }

        [Display(Name = "Emblem")]
        [Required]
        public bool HasEmblem { get; set; }

        [Display(Name = "Political Entity Type Id")]
        [Required]
        public int PoliticalEntityTypeId { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity Type")]
        public PoliticalEntityType PoliticalEntityType { get; set; }

        public ICollection<PoliticalEntityBuilder> PoliticalEntityBuilders { get; set; }

        public ICollection<PoliticalEntityFlag> PoliticalEntityFlags { get; set; }

        public ICollection<PoliticalEntitySucceeding> PrecedingEntities { get; set; }

        public ICollection<PoliticalEntitySucceeding> SucceedingEntities { get; set; }

        public ICollection<PoliticalEntityEra> PoliticalEntityEras { get; set; }

        #endregion Foreign Properties
    }
}