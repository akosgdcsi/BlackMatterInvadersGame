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
            //model = new GameModel();
        }
    }
}
