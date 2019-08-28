using MvcFactbook.Models;
using MvcFactbook.ViewModels.Models.Main;

namespace MvcFactbook.Code.Data
{
    public class BuilderDataAccess : DataAccess<Builder, BuilderView>
    {
        public BuilderDataAccess(FactbookContext context) : base(context, context.Builder) { }
    }
}
