using MvcFactbook.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityTypeView : View<PoliticalEntityType>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [StringLength(100)]
        [Required]
        public string Type => ViewObject.Type;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<PoliticalEntityView> PoliticalEntities => GetViewList<PoliticalEntityView, PoliticalEntity>(ViewObject.PoliticalEntities);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type;

        #endregion Other Properties

    }
}
