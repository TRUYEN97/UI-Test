using System;
using System.Collections.Generic;
using UiTest.Model.Cell;
using UiTest.ModelView;

namespace UiTest.Service.Factory
{
    internal class ViewModelFactory : BaseFactory<BaseSubModelView>
    {
        private static readonly Lazy<ViewModelFactory> _insatnce = new Lazy<ViewModelFactory>(() => new ViewModelFactory());
        public static ViewModelFactory Instance = _insatnce.Value;
        protected readonly Dictionary<string, BaseSubModelView> modeInstances;
        private ViewModelFactory()
        {
            modeInstances = new Dictionary<string, BaseSubModelView>();
        }

        public BaseSubModelView GetInstanceWithTypeName(string typeName, string name)
        {
            if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(name))
            {
                string key = $"{name}-{typeName}";
                if (modeInstances.TryGetValue(key, out var instance))
                {
                    return instance;
                }
                instance = base.CreateInstanceWithTypeName(typeName);
                if (instance != null)
                {
                    modeInstances[key] = instance;
                    return instance;
                }
            }
            return default;
        }
    }
}
