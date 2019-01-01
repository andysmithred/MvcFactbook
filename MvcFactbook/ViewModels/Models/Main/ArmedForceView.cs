using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class ArmedForceView : View<ArmedForce>
    {
        #region Database Properties

        public int Id => ViewObject.Id;
        public string Name => ViewObject.Name;
        public string Code => ViewObject.Code;
        public bool IsActive => ViewObject.IsActive;
        public long? Budget => ViewObject.Budget;

        #endregion Database Properties

        #region Foreign Properties

        

        #endregion Foreign Properties

        #region Other Properties

        public override string ListName => Name + ":" + Code;

        #endregion Other Properties
        
    }
}
