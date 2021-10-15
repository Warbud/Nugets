using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NuGet.Revit.Ribbon.Attribute;

namespace NuGet.Revit.Ribbon.PanelsHelper
{
    public static class Scanner
    {
        internal static Assembly Assembly;
        public static List<Type> GetTypesByPanelName(this Assembly assembly, string panelName)
        {
            Assembly = assembly;
            return assembly
                .ExportedTypes
                .Where(x =>
                {
                    var att = x.GetCustomAttribute<DisplayAttribute>();
                    if (att is null || x.IsAbstract)
                    {
                        return false;
                    }

                    return att.PanelName == panelName;
                })
                .OrderBy(x => x.GetCustomAttribute<DisplayAttribute>().Order)
                .ToList();
        }
    }
}