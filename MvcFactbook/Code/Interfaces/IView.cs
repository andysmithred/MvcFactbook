namespace MvcFactbook.Code.Interfaces
{
    public interface IView<TObject>
    {
        TObject ViewObject { get; set; }
    }
}
