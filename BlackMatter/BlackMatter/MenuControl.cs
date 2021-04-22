using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using BlackMatterRenderer;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace BlackMatter
{
    public class MenuControl: FrameworkElement
    {
        MenuRenderer renderer;
        public MenuControl()
        {
            Loaded += MenuControl_Loaded;
        }

        private void MenuControl_Loaded(object sender, RoutedEventArgs e)
        {
            renderer = new MenuRenderer();
        }

        
        
    }
}
