using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        //public ICollection<BranchFlagView> BranchFlags => GetViewList<BranchFlagView, BranchFlag>(ViewObject.BranchFlags);

        //public ICollection<ShipServiceView> ShipServices => GetViewList<ShipServiceView, ShipService>(ViewObject.ShipServices);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Type;

        #endregion Other Properties

        #region Methods

        

        #endregion Methods
    }
}
