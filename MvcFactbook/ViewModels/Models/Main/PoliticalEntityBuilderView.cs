using MvcFactbook.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class PoliticalEntityBuilderView : View<PoliticalEntityBuilder>
    {
        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        [Display(Name = "Political Entity Id")]
        public int PoliticalEntityId => ViewObject.PoliticalEntityId;

        [Required]
        [Display(Name = "Builder Id")]
        public int BuilderId => ViewObject.BuilderId;

        #endregion Database Properties

        #region Foreign Properties

        [Display(Name = "Political Entity")]
        public PoliticalEntityView PoliticalEntity => GetView<PoliticalEntityView, PoliticalEntity>(ViewObject.PoliticalEntity);

        public BuilderView Builder => GetView<BuilderView, Builder>(ViewObject.Builder);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => PoliticalEntity.ShortName + ":" + Builder.Name;

        #endregion Other Properties

    }
}
