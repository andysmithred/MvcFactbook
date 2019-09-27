using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class Shipbuilder: IComplete
    {
        #region Constructor

        public Shipbuilder()
        {
            //Ships = new HashSet<Ship>();
            //PoliticalEntityBuilders = new HashSet<PoliticalEntityBuilder>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Alternative Names")]
        public string AlternativeNames { get; set; }

        public string Parent { get; set; }

        public int? Start { get; set; }

        public int? End { get; set; }

        [Required]
        public bool Complete { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        //public ICollection<Ship> Ships { get; set; }

        //public ICollection<PoliticalEntityBuilder> PoliticalEntityBuilders { get; set; }

        #endregion Foreign Properties
    }
}