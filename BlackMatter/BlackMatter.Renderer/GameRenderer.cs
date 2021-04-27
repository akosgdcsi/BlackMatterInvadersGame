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
        Drawing backGround;
        Drawing player;
        Drawing enemy;
        Drawing bullet;
        Drawing explosion;
        GeometryDrawing Text;
        Pen red = new Pen(Brushes.Red, 2);
        Typeface font = new Typeface("Arial");
        Point textLocation = new Point(0, 1);
        Point OldPlayerPosition = new Point();
        List<Point> OldBulletPositions = new List<Point>();
        List<Point> OldEnemyPositions = new List<Point>();
        FormattedText formattedText;
        int oldWave = 0;

        public GameRenderer(IGameModel model)
        {
            this.model = model;
        }
        internal static BitmapImage GetImage(string fileName)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = Assembly.LoadFrom("BlackMatterRenderer").GetManifestResourceStream("BlackMatterRenderer.Images." + fileName);
            bmp.EndInit();
            return bmp;
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
        //Brush EnemyBrush { get { return GetBrush("enemy_1.png"); } }
        // BitmapImage BulletBrush { get { return GetImage("player_laser_1.png"); } }
        Brush ExplosionBrush { get { return GetBrush("enemy_explosion.png"); } }
        Brush BackGroundBrush { get { return GetBrush("background_2.png"); } }

        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(GetBackground());            
            dg.Children.Add(GetPlayer());
            dg.Children.Add(GetPlayerBullets());
            dg.Children.Add(GetEnemies());
            dg.Children.Add(GetExplosions());
            dg.Children.Add(GetEnemyBullets());
            dg.Children.Add(GetWaves());
            dg.Children.Add(GetLifes());

            return dg;
        }

        private Drawing GetLifes()
        {
            formattedText = new FormattedText("Life: " + model.player.Life.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, font, 20, Brushes.Black);
            Text = new GeometryDrawing(null, red, formattedText.BuildGeometry(new Point(100, 5)));

            return Text;
        }

        private Drawing GetWaves()
        {
            formattedText = new FormattedText("Wave: " + model.Wave.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, font, 20, Brushes.Black);
            Text =new GeometryDrawing(null, red, formattedText.BuildGeometry(new Point(5,5)));

            return Text;
        }

        private Drawing GetExplosions()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in model.enemies)
            {
                if (item.IsShooted)
                {
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_explosion.png"), new Rect(item.X, item.Y, 140, 140));
                    dg.Children.Add(box);
                }
            }
            explosion = dg;
            return explosion;
        }

        private Drawing GetEnemyBullets()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in model.EnemyBullets)
            {
                if (bullet == null || !OldBulletPositions.Select(x => x.Y).Contains(item.Y))
                {
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_laser.png"), new Rect(item.X,
                           item.Y, 50, 50));
                    dg.Children.Add(box);
                }
            }
            bullet = dg;
            return bullet;
        }
        private Drawing GetPlayerBullets()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in model.PlayerBullets)
            {
                if (bullet == null || !OldBulletPositions.Select(x =>x.Y).Contains(item.Y))
                {
                    //Geometry g = new RectangleGeometry(new Rect(item.X, item.Y, 50, 50));
                    //bullet = new GeometryDrawing(BulletBrush, null, g);
                    //OldBulletPositions = new Point(item.X, item.Y);
                    ImageDrawing box = new ImageDrawing(GetImage("player_laser_1.png"), new Rect(item.X,
                           item.Y, 50, 50));
                    dg.Children.Add(box);
                    
                    //return Bullet;                    
                }
                
            }
            bullet = dg;
            //if (bullet ==null)
            //{
            //    Geometry g = new RectangleGeometry(new Rect(-1, -1, 5, 5));
            //    bullet = new GeometryDrawing(BulletBrush, null, g);
            //}
            return bullet;
        }

        private Drawing GetEnemies()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in model.enemies)
            {
                if (enemy == null || !OldEnemyPositions.Select(x => x.Y).Contains(item.Y))
                {
                    //Geometry g = new RectangleGeometry(new Rect(item.X, item.Y, 140, 140));
                    //enemy = new GeometryDrawing(EnemyBrush, null, g);

                    //OldEnemyPositions = new Point(item.X, item.Y);
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_1.png"), new Rect(item.X,
                           item.Y, 140, 140));
                    dg.Children.Add(box);
                }
            }
            enemy = dg;    
            
            return enemy;
        }

        private Drawing GetPlayer()
        {
            if (player == null || OldPlayerPosition.X != model.player.X && model.player.Life==3)
            {
                //ImageDrawing image = new ImageDrawing(GetImage("player_hp3.png"), new Rect(model.player.X, model.player.Y, 80,80));
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                player = new GeometryDrawing(Player_hp3_Brush, null, g);
                
                OldPlayerPosition = new Point(model.player.X,model.player.Y);
            }
            if (player == null || OldPlayerPosition.X != model.player.X && model.player.Life == 2)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                player = new GeometryDrawing(Player_hp2_Brush, null, g);

                OldPlayerPosition = new Point(model.player.X, model.player.Y);
            }
            if (player == null || OldPlayerPosition.X != model.player.X && model.player.Life == 1)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                player = new GeometryDrawing(Player_hp1_Brush, null, g);

                OldPlayerPosition = new Point(model.player.X, model.player.Y);
            }
            if (player == null || OldPlayerPosition.X != model.player.X && model.player.Life == 0)
            {
                Geometry g = new RectangleGeometry(new Rect(model.player.X, model.player.Y, 80, 80));
                player = new GeometryDrawing(ExplosionBrush, null, g);

                OldPlayerPosition = new Point(model.player.X, model.player.Y);
            }
            return player;
        }

        private Drawing GetBackground()
        {
            if (backGround == null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, GameModel.GameWidth, GameModel.GameHeight));
                backGround = new GeometryDrawing(BackGroundBrush, null, g);
            }
            return backGround;
        }
    }
}