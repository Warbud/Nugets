using System;
using System.Drawing;

namespace NuGet.Revit.Ribbon.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DisplayAttribute : System.Attribute
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public static string DefaultImagePath { get; set; }
        public string DisplayName { get; }
        public int Order { get; }
        public string PanelName { get; }
        public Color Color { get; }
        public string Path { get; }

        public DisplayAttribute(string displayName, int order, string panelName, Color color )
        {
            if (string.IsNullOrEmpty(DefaultImagePath))
            {
                throw new ArgumentException("DefaultImagePath is null of whitespace");
            }
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Path = DefaultImagePath;
            Color = color;
        }
        
        public DisplayAttribute(string displayName, int order, string panelName )
        {
            if (string.IsNullOrEmpty(DefaultImagePath))
            {
                throw new ArgumentException("DefaultImagePath is null of whitespace");
            }
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Path = DefaultImagePath;
            Color = Color.Black;
        }
        
        public DisplayAttribute(string displayName, int order, string panelName , string path )
        {
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Path = path;
            Color = Color.Black;
        }
        
        public DisplayAttribute(string displayName, int order, string panelName, string path, Color color )
        {
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Path = path;
            Color = color;
        }
    }
}