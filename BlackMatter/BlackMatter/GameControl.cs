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

namespace BlackMatter
{
    public class GameControl:FrameworkElement
    {        
        GameModel model;
        GameLogic logic;
        GameRenderer renderer;
        DispatcherTimer dispatcherTimer;

        public GameControl()
        {
            Loaded += GameControl_Loaded; 
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            this.logic =new GameLogic(model);
            renderer = new GameRenderer(model);

            Window win = Window.GetWindow(this);
            if (win!=null)
            {
                win.KeyDown += Win_KeyDown;
                MouseDown += GameControl_MouseDown;
            }

            InvalidateVisual();
            dispatcherTimer.Start();
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
                case Key.Left: logic.PlayerMove(-1); break;
                case Key.Right: logic.PlayerMove(1); break;
                case Key.A: logic.PlayerMove(-1); break;
                case Key.D: logic.PlayerMove(1); break;
            }
        }

    }
}
