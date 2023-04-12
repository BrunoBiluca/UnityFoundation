using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityFoundation.Code
{
    public static class DependencySetupValidation
    {
        public static bool HasDependencySetup(Type t)
        {
            return t.IsGenericType
              && (
                t.GetGenericTypeDefinition() == typeof(IDependencySetup<>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,,>)
              );
        }

        public static IEnumerable<MethodInfo> GetMethods(Type t)
        {
            var interfaces = t.GetInterfaces().Where(HasDependencySetup);

            return interfaces.SelectMany(i => GetSetupMethod(t, i.GetMethod("Setup").ToString()));
        }

        public static IEnumerable<MethodInfo> GetSetupMethod(Type t, string methodName)
        {
            return t.GetMethods().Where(m => m.ToString() == methodName);
        }
    }
}
