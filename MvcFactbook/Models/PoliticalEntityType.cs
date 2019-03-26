using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Models
{
    public class PoliticalEntityType
    {
        #region Constructor

        public PoliticalEntityType()
        {
            //BranchFlags = new HashSet<BranchFlag>();
           // ShipServices = new HashSet<ShipService>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Type { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        //public ICollection<BranchFlag> BranchFlags { get; set; }

        //public ICollection<ShipService> ShipServices { get; set; }

        #endregion Foreign Properties
    }
}
