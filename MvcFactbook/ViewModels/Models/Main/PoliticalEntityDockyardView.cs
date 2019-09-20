using MvcFactbook.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityDockyardView : View<PoliticalEntityDockyard>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId => ViewObject.PoliticalEntityId;

        [Required]
        [Display(Name = "Dockyard Id")]
        public int DockyardId => ViewObject.DockyardId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntityView PoliticalEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.PoliticalEntity);

        [Display(Name = "Dockyard")]
        public DockyardView Dockyard => GetView<DockyardView, Dockyard>(ViewObject.Dockyard);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PoliticalEntity.ShortName + ":" + Dockyard.ShortName;

        #endregion Other Properties

    }
}
