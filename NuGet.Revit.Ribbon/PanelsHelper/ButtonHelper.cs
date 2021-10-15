using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using NuGet.Revit.Ribbon.Attribute;
using Autodesk.Revit.UI;

namespace NuGet.Revit.Ribbon.PanelsHelper
{
    public static class ButtonHelper
    {
        private static  Dictionary<string, string> _tooltipDictionary = new();
        private static string _errorMessage;
        /// <summary>
        /// Set tooltip dictionary for button
        /// </summary>
        /// <param name="tooltipDictionary"></param>
        /// <param name="errorMessage"></param>
        public static void SetTooltipDictionary(Dictionary<string, string> tooltipDictionary, string errorMessage = "Add description to database")
        {
            _tooltipDictionary = tooltipDictionary;
            _errorMessage = errorMessage;
        }
        
        /// <summary>
        /// Return button with drop down
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public static PulldownButtonData AddPullDownButtonData(string path, string name, Color color)
        {
            return new PulldownButtonData(name, name)
            {
                LargeImage = path.ResizeImage(32).ChangePngColor(color).ToBitmapImage(),
                Image = path.ResizeImage(16).ChangePngColor(color).ToBitmapImage(),
            };
        }
        
        /// <summary>
        /// Add large button to panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="type"></param>
        public static void AddButton(this RibbonPanel panel, Type type)
        { 
            var att = type.GetCustomAttribute<DisplayAttribute>();
            panel.AddItem(CreatePushButtonData(att.DisplayName, att.Path, type, att.Color));
        }
        
        /// <summary>
        /// Return ButtonData based on DisplayAttribute
        /// </summary>
        public static PushButtonData CreateButtonData(this Type type)
        {
            var att = type.GetCustomAttribute<DisplayAttribute>();
            return CreatePushButtonData(att.DisplayName, att.Path, type, att.Color);
        }
        
        /// <summary>
        /// Return ButtonData based on DisplayAttribute
        /// </summary>
        public static PushButtonData CreateButtonData(this Type type, string path)
        {
            var att = type.GetCustomAttribute<DisplayAttribute>();
            return CreatePushButtonData(att.DisplayName, path, type, att.Color);
        }
        
        private static PushButtonData CreatePushButtonData(string name, string path, Type type, Color color)
        {
            _tooltipDictionary.TryGetValue(name, out var tooltip);
            return new PushButtonData(name, name, Scanner.Assembly.Location, $"{type.Namespace}.{type.Name}")
            {
                LargeImage = path.ResizeImage(32).ChangePngColor(color).ToBitmapImage(),
                Image = path.ResizeImage(16).ChangePngColor(color).ToBitmapImage(),
                ToolTip = tooltip?? _errorMessage
            };
        }
        
        /// <summary>
        /// Add ButtonData to panel. Data is split in groups based on data count. 
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="data"></param>
        public static void AddPushButtonDataToPanel(this RibbonPanel panel, List<ButtonData> data)
        {
            var dataCount = data.Count;
            if (dataCount % 3 == 1)
            {
                for (var i = 0; i < dataCount - 4; i += 3)
                {
                    panel.AddStackedItems(data[i], data[i + 1], data[i + 2]);
                }
                panel.AddStackedItems(data[dataCount - 4], data[dataCount - 3]);
                panel.AddStackedItems(data[dataCount - 2], data[dataCount - 1]);
            }
            else if (dataCount % 3 == 2)
            {
                for (var i = 0; i < dataCount - 2; i += 3)
                {
                    panel.AddStackedItems(data[i], data[i + 1], data[i + 2]);
                }
                panel.AddStackedItems(data[dataCount - 2], data[dataCount - 1]);
            }
            else
            {
                for (var i = 0; i < dataCount; i += 3)
                {
                    panel.AddStackedItems(data[i], data[i + 1], data[i + 2]);
                }
            }
        }
    }
}