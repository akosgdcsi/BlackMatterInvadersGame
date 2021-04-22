using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlackMatterRenderer
{
    public class MenuRenderer
    {
        Drawing backGround;
        Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
        public MenuRenderer()
        {
            BuildDrawing();
        }
        Brush GetBrush(string fname)
        {
            if (!brushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.LoadFrom("BlackMatterRenderer").GetManifestResourceStream("BlackMatterRenderer.Images." + fname);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                brushes.Add(fname, ib);
            }
            return brushes[fname];
        }
        Brush BackGroundBrush { get { return GetBrush("background_2.png"); } }             
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(GetBackground());
            return dg;
        }

        private Drawing GetBackground()
        {
            if (backGround == null)
            {
                Geometry g = new RectangleGeometry(new Rect(0,0,800,800));
                backGround = new GeometryDrawing(BackGroundBrush, null, g);
            }
            return backGround;
        }

    }
}
