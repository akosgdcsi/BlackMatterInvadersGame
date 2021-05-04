// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;

    /// <summary>
    /// Gamerender class.
    /// </summary>
    public class GameRenderer
    {
        private IGameModel model;
        private Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
        private Drawing backGround;
        private Drawing player;
        private Drawing enemy;
        private Drawing bullet;
        private Drawing explosion;
        private GeometryDrawing text;
        private Pen red = new Pen(Brushes.Red, 2);
        private Typeface font = new Typeface("Arial");
        private Point oldPlayerPosition = new Point();
        private List<Point> oldBulletPositions = new List<Point>();
        private List<Point> oldEnemyPositions = new List<Point>();
        private FormattedText formattedText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">init model.</param>
        public GameRenderer(IGameModel model)
        {
            this.model = model;
        }

        private Brush Player_hp3_Brush
        {
            get
            {
                return this.GetBrush("player_hp3.png");
            }
        }

        private Brush Player_hp2_Brush
        {
            get
            {
                return this.GetBrush("player_hp2.png");
            }
        }

        private Brush Player_hp1_Brush
        {
            get
            {
                return this.GetBrush("player_hp1.png");
            }
        }

        // Brush EnemyBrush { get { return GetBrush("enemy_1.png"); } }
        // BitmapImage BulletBrush { get { return GetImage("player_laser_1.png"); } }
        private Brush ExplosionBrush
        {
            get
            {
                return this.GetBrush("enemy_explosion.png");
            }
        }

        private Brush BackGroundBrush
        {
            get
            {
                return this.GetBrush("background_2.png");
            }
        }

        /// <summary>
        /// gives all objects out.
        /// </summary>
        /// <returns>a drwing group.</returns>
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetPlayer());
            dg.Children.Add(this.GetPlayerBullets());
            dg.Children.Add(this.GetEnemies());
            dg.Children.Add(this.GetExplosions());
            dg.Children.Add(this.GetEnemyBullets());
            dg.Children.Add(this.GetWaves());
            dg.Children.Add(this.GetLifes());
            dg.Children.Add(this.GetScore());
            dg.Children.Add(this.GetExitButton());
            dg.Children.Add(this.GetSave());
            return dg;
        }

        private Drawing GetSave()
        {
            this.formattedText = new FormattedText("Save", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 20, Brushes.Black, 1);
            this.text = new GeometryDrawing(null, this.red, this.formattedText.BuildGeometry(new Point(727, 8)));

            return this.text;
        }

        /// <summary>
        /// Reading out images.
        /// </summary>
        /// <param name="fileName">init filename.</param>
        /// <returns>bitmapimage.</returns>
        internal static BitmapImage GetImage(string fileName)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = Assembly.LoadFrom("BlackMatterRenderer").GetManifestResourceStream("BlackMatterRenderer.Images." + fileName);
            bmp.EndInit();
            return bmp;
        }

        private Brush GetBrush(string fname)
        {
            if (!this.brushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.LoadFrom("BlackMatterRenderer").GetManifestResourceStream("BlackMatterRenderer.Images." + fname);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.brushes.Add(fname, ib);
            }

            return this.brushes[fname];
        }

        private Drawing GetExitButton()
        {
            GeometryDrawing exitBtn = new GeometryDrawing(Brushes.LightGray, new Pen(), new RectangleGeometry(new Rect(715, 5, 80, 30)));
            return exitBtn;
        }

        private Drawing GetScore()
        {
            this.formattedText = new FormattedText("Score: " + this.model.Score.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 20, Brushes.Black, 1);
            this.text = new GeometryDrawing(null, this.red, this.formattedText.BuildGeometry(new Point(180, 5)));

            return this.text;
        }

        private Drawing GetLifes()
        {
            this.formattedText = new FormattedText("Life: " + this.model.Player.Life.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 20, Brushes.Black, 1);
            this.text = new GeometryDrawing(null, this.red, this.formattedText.BuildGeometry(new Point(100, 5)));

            return this.text;
        }

        private Drawing GetWaves()
        {
            this.formattedText = new FormattedText("Wave: " + this.model.Wave.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 20, Brushes.Black, 1);
            this.text = new GeometryDrawing(null, this.red, this.formattedText.BuildGeometry(new Point(5, 5)));

            return this.text;
        }

        private Drawing GetExplosions()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in this.model.Enemies)
            {
                if (item.IsShooted)
                {
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_explosion.png"), new Rect(item.X, item.Y, 140, 140));
                    dg.Children.Add(box);
                }
            }

            this.explosion = dg;
            return this.explosion;
        }

        private Drawing GetEnemyBullets()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in this.model.EnemyBullets)
            {
                if (this.bullet == null || !this.oldBulletPositions.Select(x => x.Y).Contains(item.Y))
                {
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_laser.png"), new Rect(item.X, item.Y, 50, 50));
                    dg.Children.Add(box);
                }
            }

            this.bullet = dg;
            return this.bullet;
        }

        private Drawing GetPlayerBullets()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in this.model.PlayerBullets)
            {
                if (this.bullet == null || !this.oldBulletPositions.Select(x => x.Y).Contains(item.Y))
                {
                    ImageDrawing box = new ImageDrawing(GetImage("player_laser_1.png"), new Rect(item.X, item.Y, 50, 50));
                    dg.Children.Add(box);
                }
            }

            this.bullet = dg;

            return this.bullet;
        }

        private Drawing GetEnemies()
        {
            DrawingGroup dg = new DrawingGroup();
            foreach (var item in this.model.Enemies)
            {
                if (this.enemy == null || !this.oldEnemyPositions.Select(x => x.Y).Contains(item.Y))
                {
                    ImageDrawing box = new ImageDrawing(GetImage("enemy_1.png"), new Rect(item.X, item.Y, 140, 140));
                    dg.Children.Add(box);
                }
            }

            this.enemy = dg;

            return this.enemy;
        }

        private Drawing GetPlayer()
        {
            if (this.player == null || this.oldPlayerPosition.X != this.model.Player.X && this.model.Player.Life == 3)
            {
                Geometry g = new RectangleGeometry(new Rect(this.model.Player.X, this.model.Player.Y, 80, 80));
                this.player = new GeometryDrawing(this.Player_hp3_Brush, null, g);

                this.oldPlayerPosition = new Point(this.model.Player.X, this.model.Player.Y);
            }

            if (this.player == null || this.oldPlayerPosition.X != this.model.Player.X && this.model.Player.Life == 2)
            {
                Geometry g = new RectangleGeometry(new Rect(this.model.Player.X, this.model.Player.Y, 80, 80));
                this.player = new GeometryDrawing(this.Player_hp2_Brush, null, g);

                this.oldPlayerPosition = new Point(this.model.Player.X, this.model.Player.Y);
            }

            if (this.player == null || this.oldPlayerPosition.X != this.model.Player.X && this.model.Player.Life == 1)
            {
                Geometry g = new RectangleGeometry(new Rect(this.model.Player.X, this.model.Player.Y, 80, 80));
                this.player = new GeometryDrawing(this.Player_hp1_Brush, null, g);

                this.oldPlayerPosition = new Point(this.model.Player.X, this.model.Player.Y);
            }

            if (this.player == null || this.oldPlayerPosition.X != this.model.Player.X && this.model.Player.Life == 0)
            {
                Geometry g = new RectangleGeometry(new Rect(this.model.Player.X, this.model.Player.Y, 80, 80));
                this.player = new GeometryDrawing(this.ExplosionBrush, null, g);

                this.oldPlayerPosition = new Point(this.model.Player.X, this.model.Player.Y);
            }

            return this.player;
        }

        private Drawing GetBackground()
        {
            if (this.backGround == null)
            {
                Geometry g = new RectangleGeometry(new Rect(0, 0, GameModel.GameWidth, GameModel.GameHeight));
                this.backGround = new GeometryDrawing(this.BackGroundBrush, null, g);
            }

            return this.backGround;
        }
    }
}