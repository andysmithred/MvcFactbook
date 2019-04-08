using MvcFactbook.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntitySucceedingView : View<PoliticalEntitySucceeding>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Preceding Entity Id")]
        public int PoliticalEntityId => ViewObject.PoliticalEntityId;

        [Required]
        [Display(Name = "Succeeding Entity Id")]
        public int SucceedingPoliticalEntityId => ViewObject.SucceedingPoliticalEntityId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Preceding Entity")]
        public PoliticalEntityView PrecedingEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.PrecedingPoliticalEntity);

        [Display(Name = "Succeeding Entity")]
        public PoliticalEntityView SucceedingEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.SucceedingPoliticalEntity);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PrecedingEntity.Name + ":" + SucceedingEntity.Name;

        #endregion Other Properties

        #region Methods

        #endregion Methods
    }
}
