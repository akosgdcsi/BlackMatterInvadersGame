using BlackMatter.Logic;
using BlackMatter.Model;
using BlackMatter.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using BlackMatter.Model.Interfaces;
using BlackMatter.Logic.Interfaces;

namespace BlackMatter
{
    public class GameControl:FrameworkElement
    {
        IGameModel model;
        IGameLogic logic;
        GameRenderer renderer;
        DispatcherTimer dispatcherTimer;
        DispatcherTimer enemyMover;
        DispatcherTimer bulletMover;
        DispatcherTimer enemybulletMove;

        public GameControl()
        {
            Loaded += GameControl_Loaded; 
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            bulletMover = new DispatcherTimer();
            enemyMover = new DispatcherTimer();
            enemybulletMove = new DispatcherTimer();
            this.logic =new GameLogic(model);
            model = logic.InitModel();
            renderer = new GameRenderer(model);

            Window win = Window.GetWindow(this);
            if (win!=null)
            {
                win.KeyDown += Win_KeyDown;
                MouseDown += GameControl_MouseDown;
            }
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50);
            dispatcherTimer.Tick += DispatcherTimer_Tick;                       
            enemyMover.Interval = TimeSpan.FromMilliseconds(2000);
            enemyMover.Tick += EnemyMover_Tick;
            enemybulletMove.Interval = TimeSpan.FromMilliseconds(4000);
            enemybulletMove.Tick += EnemybulletMove_Tick;

            enemybulletMove.Start();
            enemyMover.Start();
            dispatcherTimer.Start();
            


            InvalidateVisual();
        }

        private void GameOver()
        {
            if(model.Player.Life==0)
            {
                dispatcherTimer.Stop();
                enemyMover.Stop();
                enemybulletMove.Stop();
                MessageBox.Show("Game Over!\n\nHighscore: "+model.Score, "GameOver", MessageBoxButton.OK, MessageBoxImage.Hand);
                Window win = Window.GetWindow(this);
                win.Close();
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.Show();
            }
            
        }
        private void EnemybulletMove_Tick(object sender, EventArgs e)
        {
            EnemyShoot();
            InvalidateVisual();
        }

        private void EnemyShoot_Tick(object sender, EventArgs e)
        {
            logic.Enemyshoot();
            InvalidateVisual();
        }

        private void PlayerShoot()
        {
            Bullet bullet = logic.Shoot();
            model.PlayerBullets.Add(bullet);
            bullet.Timer = new DispatcherTimer();
            bullet.Timer.Interval = TimeSpan.FromMilliseconds(30);
            bullet.Timer.Tick += delegate {
                logic.BulletMove(ref bullet);
            };
            bullet.Timer.Start();
        }
        private void EnemyShoot()
        {
            if (model.Enemies.Count !=0)
            {
                Bullet bullet = logic.Enemyshoot2();
                model.EnemyBullets.Add(bullet);
                bullet.Timer = new DispatcherTimer();
                bullet.Timer.Interval = TimeSpan.FromMilliseconds(5);
                bullet.Timer.Tick += delegate {
                    logic.EnemyBulletMove2(ref bullet);
                };
                bullet.Timer.Start();
            }
            

        }

        private void EnemyMover_Tick(object sender, EventArgs e)
        {
            logic.EnemyMove();   
            InvalidateVisual();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            GameOver();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (renderer != null) drawingContext.DrawDrawing(renderer.BuildDrawing());                        
        }

        private void GameControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            logic.PlayerMove((int)e.GetPosition(this).X);
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left: logic.PlayerMove(-25); break;
                case Key.Right: logic.PlayerMove(25); break;
                case Key.A: logic.PlayerMove(-50); break;
                case Key.D: logic.PlayerMove(50); break;
                case Key.Space: PlayerShoot(); break;
            }
        }

    }
}
