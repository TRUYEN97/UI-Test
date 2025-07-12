
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace UiTest.Service.Factory
{
    public abstract class BaseFactory<T>
    {
        protected readonly Dictionary<string, T> modeInstances;
        protected readonly Dictionary<string, Type> types;
        protected BaseFactory()
        {
            modeInstances = new Dictionary<string, T>();
            Type baseType = typeof(T);
            types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToDictionary(t=> t.Name, t => t);
        }

        protected T GetInstanceWithTypeName(string name, string typeName, params object[] args)
        {
            if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(name))
            {
                string key = $"{name}-{typeName}";
                if (modeInstances.TryGetValue( key, out var instance))
                {
                    return instance;
                }
                if (types.TryGetValue(typeName, out var type))
                {
                    T ins = CreateInstance(type, args);
                    modeInstances[key] = ins;
                    return ins;
                }
            }
            return default;
        }

        public bool Exists(string typeName)
        {
            return types.ContainsKey(typeName);
        }

        protected T CreateInstanceWithTypeName(string typeName, params object[] args)
        {
            if (!string.IsNullOrEmpty(typeName))
            {
                if (types.TryGetValue(typeName, out var type))
                {
                    T ins = CreateInstance(type, args);
                    return ins;
                }
            }
            return default;
        }
        protected virtual T CreateInstance(Type type, params object[] args)
        {
            if (type == null)
            {
                return default;
            }
            return (T) Activator.CreateInstance(type, args);
        }
    }
}
