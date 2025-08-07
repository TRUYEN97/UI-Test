
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using UiTest.Exceptions;

namespace UiTest.Service.Factory
{
    public abstract class BaseFactory<T>
    {
        protected readonly Dictionary<string, Type> types;
        protected BaseFactory()
        {
            Type baseType = typeof(T);
            if (baseType.IsGenericType)
            {
                Type baseGenericType = typeof(T).GetGenericTypeDefinition();
                types = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && IsSubclassOfRawGeneric(baseGenericType, t))
                    .ToDictionary(t => t.Name, t => t);
            }
            else
            {
                types = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => baseType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToDictionary(t => t.Name, t => t);
            }
        }
        private bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        public bool Exists(string typeName)
        {
            return types.ContainsKey(typeName);
        }

        public T CreateInstanceWithTypeName(string typeName, params object[] args)
        {
            if (!string.IsNullOrEmpty(typeName))
            {
                if (types.TryGetValue(typeName, out var type))
                {
                    T ins = CreateInstance(type, args);
                    return ins;
                }
            }
            throw new FactoryClassTypeNotFoundException($"{typeName} not exsits");
        }
        public static T CreateInstance(Type type, params object[] args)
        {
            if (type == null)
            {
                return default;
            }
            return (T) Activator.CreateInstance(type, args);
        }
    }
}
