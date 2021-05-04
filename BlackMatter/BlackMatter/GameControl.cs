// <copyright file="GameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using BlackMatter.Logic;
    using BlackMatter.Logic.Interfaces;
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;
    using BlackMatter.Renderer;
    using BlackMatter.Repository;

    /// <summary>
    /// gamecontrol class.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private IGameModel model;
        private IGameLogic logic;
        private SaveLogic saveLogic;
        private GameRenderer renderer;
        private DispatcherTimer dispatcherTimer;
        private DispatcherTimer enemyMover;
        private DispatcherTimer bulletMover;
        private DispatcherTimer enemybulletMove;
        private DispatcherTimer wait;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Gets or sets a name.
        /// </summary>
        public static string PlayerName { get; set; }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.dispatcherTimer = new DispatcherTimer();
            this.bulletMover = new DispatcherTimer();
            this.enemyMover = new DispatcherTimer();
            this.enemybulletMove = new DispatcherTimer();
            this.wait = new DispatcherTimer();
            this.saveLogic = new SaveLogic(new SaveInstance(), this.model);
            if (this.saveLogic.LoadGame() == null)
            {
                this.logic = new GameLogic(this.model);
                this.model = this.logic.InitModel();
            }
            else
            {
                this.model = this.saveLogic.LoadGame();
                this.logic = new GameLogic(this.model);
                this.saveLogic.DeleteSave();
            }

            this.renderer = new GameRenderer(this.model);
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                win.KeyDown += this.Win_KeyDown;
                win.MouseLeftButtonDown += this.Win_MouseLeftButtonDown;
            }

            this.dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50);
            this.dispatcherTimer.Tick += this.DispatcherTimer_Tick;
            this.enemyMover.Interval = TimeSpan.FromMilliseconds(2000);
            this.enemyMover.Tick += this.EnemyMover_Tick;
            this.enemybulletMove.Interval = TimeSpan.FromMilliseconds(4000);
            this.enemybulletMove.Tick += this.EnemybulletMove_Tick;
            this.enemybulletMove.Start();
            this.enemyMover.Start();
            this.dispatcherTimer.Start();

            this.InvalidateVisual();
        }

        private void Win_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.GetPosition(this).X >= 715 && e.GetPosition(this).X <= 795 && e.GetPosition(this).Y >= 5 && e.GetPosition(this).Y <= 35)
            {
                this.dispatcherTimer.Stop();
                this.enemyMover.Stop();
                this.enemybulletMove.Stop();
                foreach (var item in this.model.PlayerBullets.ToList())
                {
                    item.Timer.Stop();
                    this.model.PlayerBullets.Remove(item);
                }

                foreach (var item in this.model.EnemyBullets.ToList())
                {
                    item.Timer.Stop();
                    this.model.EnemyBullets.Remove(item);
                }

                this.saveLogic = new SaveLogic(new SaveInstance(), this.model);
                this.saveLogic.SaveInstance();
                Window win = Window.GetWindow(this);
                win.Close();
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.Show();
            }
        }

        private void Wait_Tick(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        private void GameOver()
        {
            if (this.model.Player.Life == 0)
            {
                this.dispatcherTimer.Stop();
                this.enemyMover.Stop();
                this.enemybulletMove.Stop();

                // MessageBox.Show("Game Over!\n\nHighscore: " + this.model.Score, "GameOver", MessageBoxButton.OK, MessageBoxImage.Hand);
                NameAsk nameAsk = new NameAsk();
                nameAsk.ShowDialog();
                HighScoreRepository highScore = new HighScoreRepository();
                this.saveLogic = new SaveLogic(highScore, this.model);
                this.saveLogic.HighscoreInstance(PlayerName);
                Window win = Window.GetWindow(this);
                win.Close();
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.Show();
            }
        }

        private void EnemybulletMove_Tick(object sender, EventArgs e)
        {
            this.EnemyShoot();
            this.InvalidateVisual();
        }

        private void EnemyShoot_Tick(object sender, EventArgs e)
        {
            this.logic.Enemyshoot();
            this.InvalidateVisual();
        }

        private void PlayerShoot()
        {
            Bullet bullet = this.logic.Shoot();
            this.model.PlayerBullets.Add(bullet);
            bullet.Timer = new DispatcherTimer();
            bullet.Timer.Interval = TimeSpan.FromMilliseconds(30);
            bullet.Timer.Tick += delegate
            {
                this.logic.BulletMove(ref bullet);
            };
            bullet.Timer.Start();
        }

        private void EnemyShoot()
        {
            if (this.model.Enemies.Count != 0)
            {
                Bullet bullet = this.logic.Enemyshoot2();
                this.model.EnemyBullets.Add(bullet);
                bullet.Timer = new DispatcherTimer();
                bullet.Timer.Interval = TimeSpan.FromMilliseconds(5);
                bullet.Timer.Tick += delegate
                {
                    this.logic.EnemyBulletMove2(ref bullet);
                };
                bullet.Timer.Start();
            }
        }

        private void EnemyMover_Tick(object sender, EventArgs e)
        {
            if (this.model.Enemiesinthiswave != 0 || this.model.Enemies.Count != 0)
            {
                this.logic.EnemyMove();
                this.InvalidateVisual();
            }
            else
            {
                this.logic.NextWave();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (this.model.Player.Life == 0)
            {
                this.logic.PlayerMove(-1);
                this.InvalidateVisual();
                this.GameOver();
            }

            this.InvalidateVisual();
        }

        /// <summary>
        /// overrides OnRender.
        /// </summary>
        /// <param name="drawingContext">init drawingContext.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing());
            }
        }

        private void GameControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.logic.PlayerMove((int)e.GetPosition(this).X);
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: this.logic.PlayerMove(-25); break;
                case Key.Right: this.logic.PlayerMove(25); break;
                case Key.A: this.logic.PlayerMove(-50); break;
                case Key.D: this.logic.PlayerMove(50); break;
                case Key.Space: this.PlayerShoot(); break;
            }
        }
    }
}