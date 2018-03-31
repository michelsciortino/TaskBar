using System;
using System.Reflection;

namespace TaskBar.Core.Helpers
{
    public static class UriHelper
    {
        public static Uri GetUri(Type baseType, string relativePath)
        {
            Assembly oAssembly = Assembly.GetCallingAssembly();
            AssemblyName oName = oAssembly.GetName();
            return new Uri(
                    String.Format(
                        "pack://application:,,,/{0};v{1};component/{2}",
                        oName.Name,
                        oName.Version.ToString(),
                        relativePath),
                    UriKind.Absolute);
        }
    }
}
