using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;

namespace MvcFactbook.Code.Data
{
    public class ShipClassDataAccess : DataAccess<ShipClass, ShipClassView>
    {
        public ShipClassDataAccess(FactbookContext context) : base(context, context.ShipClass) { }
    }
}
