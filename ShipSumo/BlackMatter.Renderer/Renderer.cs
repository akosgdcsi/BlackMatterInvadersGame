using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BlackMatter.Model;


namespace BlackMatter.Renderer
{
    public class Renderer
    {
        GameModel model;
        Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
        Drawing BackGround;

        public Renderer(GameModel model)
        {
            this.model = model;
            
        }
        Brush GetBrush(string fname)
        {
            if (!brushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                brushes.Add(fname, ib);
            }
            return brushes[fname];
        }

        Brush PlayerBrush { get { return GetBrush("BlackMatter.Renderer.Images.player_ship.png"); } }

        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(GetBackground());
        }

        private Drawing GetBackground()
        {
            if(BackGround ==null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, model.GameWidth, model.GameHeight));
                BackGround = new GeometryDrawing(Brushes.Black, null, g);
            }
            return BackGround;
        }
    }
}
