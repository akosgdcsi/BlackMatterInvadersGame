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
        Drawing Player;
        Drawing Enemy;
        Drawing Bullet;
        Drawing Text;
        Pen red = new Pen(Brushes.Red, 2);
        Typeface font = new Typeface("Arial");
        Point textLocation = new Point(0, 1);
        FormattedText formattedText;
        int oldWave = -1;

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
            dg.Children.Add(GetPlayer());
            dg.Children.Add(GetEnemies());
            dg.Children.Add(GetBullets());
            //dg.Children.Add(GetWaves());

            return dg;
        }
        /*
        private Drawing GetWaves()
        {
            if (oldWave!=model.Wave)
            {
                formattedText = new FormattedText(model.Wave.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, font, 16, Brushes.Red, 1);
                Text;
            }
            return formattedText;
        }*/

        private Drawing GetBullets()
        {
            if (Bullet == null)
            {
                Geometry g = new RectangleGeometry(new Rect(305,400, 5, 5));
                Enemy = new GeometryDrawing(Brushes.Red, null, g);
            }
            return Enemy;
        }

        private Drawing GetEnemies()
        {
            if (Enemy == null)
            {
                Geometry g = new RectangleGeometry(new Rect(300,400, 25, 25));
                Enemy= new GeometryDrawing(Brushes.Red, null, g);
            }
            return Enemy;
        }

        private Drawing GetPlayer()
        {
            if (Player == null)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 25,25 ));
                Player = new GeometryDrawing(PlayerBrush, null, g);                
            }
            return Player;
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
