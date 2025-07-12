using System;
using UiTest.Model.Cell;
using UiTest.ModelView;

namespace UiTest.Service.Factory
{
    internal class ViewModelFactory : BaseFactory<BaseSubModelView>
    {
        private static readonly Lazy<ViewModelFactory> _insatnce = new Lazy<ViewModelFactory>(() => new ViewModelFactory());
        public static ViewModelFactory Instance = _insatnce.Value;
        private ViewModelFactory() { }

        public BaseSubModelView GetInstanceWithTypeName(string typeName, string name)
        {
            return base.GetInstanceWithTypeName(name, typeName);
        }
    }
}
