using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Data
{
    public class ShipServiceDataAccess : DataAccess<ShipService, ShipServiceView>
    {
        public ShipServiceDataAccess(FactbookContext context) : base(context, context.ShipService) { }
    }
}
