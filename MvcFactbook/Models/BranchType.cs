using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public partial class BranchType
    {
        #region Constructor

        public BranchType()
        {
            Branches = new HashSet<Branch>();
        }

        #endregion Constructor

        #region Database Properties

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }

        [StringLength(5)]
        [Required]
        public string Code { get; set; }

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<Branch> Branches { get; set; }

        #endregion Foreign Properties
    }
}
