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
        int oldWave = 0;

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

        Brush Player_hp3_Brush { get { return GetBrush("player_hp3.png"); } }
        Brush Player_hp2_Brush { get { return GetBrush("player_hp2.png"); } }
        Brush Player_hp1_Brush { get { return GetBrush("player_hp1.png"); } }
        Brush EnemyBrush { get { return GetBrush("enemy_1.png"); } }
        Brush BulletBrush { get { return GetBrush("player_laser_1.png"); } }
        Brush ExplosionBrush { get { return GetBrush("enemy_explosion.png"); } }
        Brush BackGroundBrush { get { return GetBrush("background.png"); } }

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
                    Geometry g = new RectangleGeometry(new Rect(item.X+22.5, item.Y-3, 50, 50));
                    Bullet = new GeometryDrawing(BulletBrush, null, g);
                    OldBulletPosition = new Point(item.X, item.Y);


                    //return Bullet;                    
                }
            }
            if (Bullet ==null)
            {
                Geometry g = new RectangleGeometry(new Rect(-1, -1, 5, 5));
                Bullet = new GeometryDrawing(BulletBrush, null, g);
            }
            return Bullet;
        }

        private Drawing GetEnemies()
        {
            
            foreach(var item in model.enemies)
            {
                if (Enemy == null || OldEnemyPosition.Y!= item.Y)
                {
                    Geometry g = new RectangleGeometry(new Rect(item.X, item.Y, 140, 140));
                    Enemy = new GeometryDrawing(EnemyBrush, null, g);

                    OldEnemyPosition = new Point(item.X, item.Y);
                    return Enemy;
                }
            }
                
            
            return Enemy;
        }

        private Drawing GetPlayer()
        {
            if (Player == null || OldPlayerPosition.X != model.player.X || model.player.Life==3)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                Player = new GeometryDrawing(Player_hp3_Brush, null, g);
                
                OldPlayerPosition = new Point(model.player.X,model.player.Y);
            }
            if (Player == null || OldPlayerPosition.X != model.player.X || model.player.Life == 2)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                Player = new GeometryDrawing(Player_hp2_Brush, null, g);

                OldPlayerPosition = new Point(model.player.X, model.player.Y);
            }
            if (Player == null || OldPlayerPosition.X != model.player.X || model.player.Life == 1)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                Player = new GeometryDrawing(Player_hp1_Brush, null, g);

                OldPlayerPosition = new Point(model.player.X, model.player.Y);
            }

            return Player;
        }

        private Drawing GetBackground()
        {
            if (BackGround == null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, GameModel.GameWidth, GameModel.GameHeight));
                BackGround = new GeometryDrawing(BackGroundBrush, null, g);
            }
            return BackGround;
        }
    }
}