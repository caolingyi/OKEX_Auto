using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Utilities
{
    public class FileHelper
    {
        public static Type GetAbsolutePath(string assemblyName, string className)
        {
            Assembly assembly = Assembly.Load(new AssemblyName(assemblyName));
            Type type = assembly.GetType(className);
            return type;
        }
    }
}
