using BlackMatter.Model;
using BlackMatter.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlackMatter.Renderer
{
    public class GameRenderer
    {
        IGameModel model;
        Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
        Drawing BackGround;
        Drawing Player;
        Drawing Enemy;
        Drawing Bullet;
        Drawing Text;
        Pen red = new Pen(Brushes.Red, 2);
        Typeface font = new Typeface("Arial");
        Point textLocation = new Point(0, 1);
        Point OldPlayerPosition = new Point();
        Point OldBulletPosition = new Point();
        Point OldEnemyPosition = new Point();
        FormattedText formattedText;
        int oldWave = -1;

        public GameRenderer(IGameModel model)
        {
            this.model = model;
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

        Brush PlayerBrush { get { return GetBrush("player_ship.png"); } }

        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(GetBackground());            
            dg.Children.Add(GetPlayer());
            dg.Children.Add(GetPlayerBullets());
            dg.Children.Add(GetEnemies());
            dg.Children.Add(GetEnemyBullets());
            //dg.Children.Add(GetWaves());

            return dg;
        }
        

        private Drawing GetEnemyBullets()
        {
            foreach (var item in model.EnemyBullets)
            {
                if (Bullet == null || OldBulletPosition.Y != item.Y)
                {
                    Geometry g = new RectangleGeometry(new Rect(item.X, item.Y, 5, 5));
                    Bullet = new GeometryDrawing(Brushes.Yellow, null, g);
                    OldBulletPosition = new Point(item.X, item.Y);
                }
            }
            return Bullet;
        }
        private Drawing GetPlayerBullets()
        {
            foreach (var item in model.PlayerBullets)
            {
                if (Bullet == null || OldBulletPosition.Y != item.Y)
                {
                    Geometry g = new RectangleGeometry(new Rect(item.X+22.5, item.Y-3, 5, 5));
                    Bullet = new GeometryDrawing(Brushes.Yellow, null, g);
                    OldBulletPosition = new Point(item.X, item.Y);
                }
            }
            if (Bullet ==null)
            {
                Geometry g = new RectangleGeometry(new Rect(-1, -1, 5, 5));
                Bullet = new GeometryDrawing(Brushes.Yellow, null, g);
            }
            return Bullet;
        }

        private Drawing GetEnemies()
        {
            
            foreach(var item in model.enemies)
            {
                if (Enemy == null || OldEnemyPosition.Y!= item.Y)
                {
                    Geometry g = new RectangleGeometry(new Rect(item.X, item.Y, 50, 50));
                    Enemy = new GeometryDrawing(Brushes.Red, null, g);

                    OldEnemyPosition = new Point(item.X, item.Y);
                    return Enemy;
                }
            }
                
            
            return Enemy;
        }

        private Drawing GetPlayer()
        {
            if (Player == null || OldPlayerPosition.X != model.player.X)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 50, 50));
                Player = new GeometryDrawing(PlayerBrush, null, g);

                OldPlayerPosition = new Point(model.player.X,model.player.Y);
            }
            return Player;
        }

        private Drawing GetBackground()
        {
            if (BackGround == null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, GameModel.GameWidth, GameModel.GameHeight));
                BackGround = new GeometryDrawing(Brushes.Black, null, g);
            }
            return BackGround;
        }
    }
}