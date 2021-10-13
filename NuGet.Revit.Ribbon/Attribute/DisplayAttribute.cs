using System;
using System.Drawing;

namespace NuGet.Revit.Ribbon.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisplayAttribute : System.Attribute
    {
        //private static readonly Random Random = new Random();
        public string DisplayName { get; }
        public int Order { get; }
        public string PanelName { get; }
        public Color Color { get; }

        public DisplayAttribute(string displayName, int order, string panelName )
        {
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Color = Color.Black;
            // var localRandom = Random.Next(255);
            //
            // var hslColor = new Hsl
            // {
            //     H = Random.Next(170,260),
            //     S = Random.Next(80,100),
            //     L = Random.Next(40,60)
            // };
            // var rgb = hslColor.ToRgb();
            //
            // Color = Color.FromArgb((int)rgb.R, (int)rgb.G, (int)rgb.B);
        }
        
        public DisplayAttribute(string displayName, int order, string panelName, Color color )
        {
            DisplayName = displayName;
            Order = order;
            PanelName = panelName;
            Color = color;
        }
    }
}