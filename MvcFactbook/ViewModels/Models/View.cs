using MvcFactbook.Code.Interfaces;
using System.Collections.Generic;

namespace MvcFactbook.ViewModels.Models
{
    public abstract class View<T> : IView<T>, IListName
    {
        public View() { }

        public View(T viewObject)
        {
            ViewObject = viewObject;
        }

        public T ViewObject { get; set; }

        public abstract string ListName { get; }

        protected ICollection<TView> GetViewList<TView, TObject>(IEnumerable<TObject> list)
            where TView : IView<TObject>, new()
        {
            ICollection<TView> result = new List<TView>();

            foreach (var item in list)
            {
                TView view = new TView();
                view.ViewObject = item;
                result.Add(view);
            }

            return result;
        }

        protected TView GetView<TView, TObject>(TObject item)
            where TView : View<TObject>, new()
        {
            if (item != null)
            {
                TView result = new TView();
                result.ViewObject = item;
                return result;
            }
            else
            {
                return null;
            }
        }

        protected ICollection<IMapItem> GetMapItems<TObject>(IEnumerable<TObject> list)
            where TObject : IMapItem
        {
            ICollection<IMapItem> result = new List<IMapItem>();

            foreach (var item in list)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
