using MvcFactbook.Code.Classes;
using MvcFactbook.Code.Data;
using MvcFactbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.ViewModels.Models.Main
{
    public class HomeView
    {
        #region Private Declarations

        private FactbookContext context = null;
        private CompleteItem<ShipClass> shipClasses = null;
        private CompleteItem<Ship> ships = null;
        private CompleteItem<ShipService> shipServices = null;
        private CompleteItem<Builder> builders = null;

        #endregion Private Declarations

        #region Public Properties

        public FactbookContext Context
        {
            get => context ?? throw new NullReferenceException("The Factbook Context object has not been set.");
            set => context = value;
        }

        public CompleteItem<ShipClass> ShipClasses
        {
            get => shipClasses ?? (shipClasses = new CompleteItem<ShipClass>(Context.ShipClass));
            set => shipClasses = value;
        }

        public CompleteItem<Ship> Ships
        {
            get => ships ?? (ships = new CompleteItem<Ship>(Context.Ship));
            set => ships = value;
        }

        public CompleteItem<ShipService> ShipServices
        {
            get => shipServices ?? (shipServices = new CompleteItem<ShipService>(Context.ShipService));
            set => shipServices = value;
        }

        public CompleteItem<Builder> Builders
        {
            get => builders ?? (builders = new CompleteItem<Builder>(Context.Builder));
            set => builders = value;
        }

        #endregion Public Properties

        #region Constructor

        public HomeView(FactbookContext context)
        {
            Context = context;
        }

        #endregion Constructor

    }
}
