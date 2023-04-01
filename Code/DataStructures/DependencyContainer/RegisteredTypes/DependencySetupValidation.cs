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

            foreach(var i in interfaces)
            {
                var methods = t.GetMethods()
                    .Where(m => m.ToString() == i.GetMethod("Setup").ToString());

                foreach(var method in methods)
                    yield return method;
            }
        }
    }
}
