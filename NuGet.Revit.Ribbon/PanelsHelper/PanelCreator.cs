using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using NuGet.Revit.Ribbon.Attribute;
using Autodesk.Revit.UI;

namespace NuGet.Revit.Ribbon.PanelsHelper
{
    public class PanelCreator
    {
        private readonly Assembly _dll;
        private string _folderPath;
        public PanelCreator(Assembly dll, string folderPath)
        {
            _dll = dll;
            _folderPath = folderPath;
        }
        
        
        public List<Type> GetTypesByAttribute(string panelName)
        {
            return _dll
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
        
        /// <summary>
        /// Button with drop down
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        internal static PulldownButtonData AddPullDownButtonData(string path, string name, Color color)
        {
            return new PulldownButtonData(name, name)
            {
                LargeImage = path.TransformImage(32).ChangePngColor(color).ToBitmapImage(),
                Image = path.TransformImage(16).ChangePngColor(color).ToBitmapImage(),
            };
        }
    }
}