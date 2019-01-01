using MvcFactbook.Code.Interfaces;
using MvcFactbook.Models;
using System;

namespace MvcFactbook.Controllers
{
    public abstract class FactbookController<T, TView> : BaseController<T, TView>
        where T : class
        where TView : IView<T>, new()
    {
        #region Private Declarations

        private FactbookContext context = null;

        #endregion Private Declarations

        #region Properties

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The Factbook Context object has not been set.");
            set => context = value;
        }

        #endregion Properties

        #region Constructor

        public FactbookController(FactbookContext context)
        {
            Context = context;
        }

        #endregion Constructor
    }
}