using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.Models
{
    public class PoliticalEntityType
    {
        #region Constructor

        public PoliticalEntityType()
        {
            PoliticalEntities = new HashSet<PoliticalEntity>();
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

        public ICollection<PoliticalEntity> PoliticalEntities { get; set; }

        #endregion Foreign Properties
    }
}
