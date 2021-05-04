// <copyright file="MainMenuWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
    using System.Windows;
    using BlackMatter.Logic;
    using BlackMatter.Model.Interfaces;
    using BlackMatter.Repository;

    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml.
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuWindow"/> class.
        /// </summary>
        public MainMenuWindow()
        {
            this.InitializeComponent();
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.ShowDialog() == true)
            {
                mainWindow.Show();
            }

            this.Close();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HighScoreWin(object sender, RoutedEventArgs e)
        {
            HighScoreWindow scoreWindow = new HighScoreWindow();
            if (scoreWindow.ShowDialog() == true)
            {
                scoreWindow.Show();
            }
        }

        private void ContinueGame(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.ShowDialog() == true)
            {
                mainWindow.Show();
            }
        }
    }
}