using MvcFactbook.Code.Classes;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ShipGroupView : View<ShipGroup>
    {
        #region Private Declarations

        private const string ICON_PATH = "/images/icons/";
        private const string ICON_EXTENSION = ".png";

        #endregion Private Declarations

        #region Database Properties

        [Key]
        public int Id => ViewObject.Id;

        [Required]
        public string Name => ViewObject.Name;

        public string Description => ViewObject.Description;

        [StringLength(20)]
        public string Icon => ViewObject.Icon;

        #endregion Database Properties

        #region Foreign Properties

        public ICollection<ShipGroupSetView> ShipGroupSets => GetViewList<ShipGroupSetView, ShipGroupSet>(ViewObject.ShipGroupSets);

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name;
        public string DescriptionLabel => String.IsNullOrEmpty(Description) ? "--" : Description;
        public string IconLabel => String.IsNullOrEmpty(Icon) ? "--" : Icon;
        public string IconLight => Icon + "-light.png";
        public string IconDark => Icon + "-dark.png";
        public string IconLightFullPath => Path.Combine(ICON_PATH + IconLight);
        public string IconDarkFullPath => Path.Combine(ICON_PATH + IconDark);

        public ICollection<ShipServiceView> ShipServices => ShipGroupSets.Select(f => f.ShipService).Distinct(f => f.Id).ToList();

        #endregion Other Properties
    }
}
