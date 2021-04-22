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

        public GameControl()
        {
            Loaded += GameControl_Loaded; 
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            bulletMover = new DispatcherTimer();
            enemyMover = new DispatcherTimer();
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
            
            
            enemyMover.Start();
            dispatcherTimer.Start();



            InvalidateVisual();
        }
        private void PlayerShoot()
        {
            Bullet bullet = logic.Shoot();
            model.PlayerBullets.Add(bullet);
            bulletMover.Interval = TimeSpan.FromMilliseconds(5);
            bulletMover.Tick += delegate {
                logic.BulletMove(ref bullet);
            };
            bulletMover.Start();
        }
        private void BulletMover_Tick(object sender, EventArgs e)
        {
            
            
            InvalidateVisual();
        }

        private void EnemyMover_Tick(object sender, EventArgs e)
        {
            logic.EnemyMove();   
            InvalidateVisual();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {                        
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
                case Key.Left: logic.PlayerMove(-10); break;
                case Key.Right: logic.PlayerMove(10); break;
                case Key.A: logic.PlayerMove(-10); break;
                case Key.D: logic.PlayerMove(10); break;
                case Key.Space: PlayerShoot(); break;
            }
        }

    }
}
