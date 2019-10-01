using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Dockyard : IComplete
    {
        #region Constructor

        public Dockyard()
        {
            Ships = new HashSet<Ship>();
            PoliticalEntityDockyards = new HashSet<PoliticalEntityDockyard>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Complete")]
        public bool Complete { get; set; }

        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Alternative Names")]
        public string AlternativeNames { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }

        [Display(Name = "Zoom")]
        public int? Zoom { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<Ship> Ships { get; set; }

        public ICollection<PoliticalEntityDockyard> PoliticalEntityDockyards { get; set; }

        public ICollection<DockyardHistory> DockyardHistory { get; set; }

        #endregion Foreign Properties
    }
}
