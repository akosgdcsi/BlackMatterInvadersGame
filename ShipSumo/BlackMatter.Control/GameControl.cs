using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BlackMatter.Model;
using BlackMatter.Logic;
using BlackMatter.Renderer;



namespace BlackMatter.Control
{
    public class GameControl
    {
        GameModel model;
        GameLogic logic;
        GameRenderer renderer;
        DispatcherTimer

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
