// <copyright file="LoadGameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml.
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadGameWindow"/> class.
        /// </summary>
        public LoadGameWindow()
        {
            this.InitializeComponent();
        }

        private void FillListBox()
        {
            /*
            HighScoreRepository highScore = new HighScoreRepository();
            List<Highscore> ls = highScore.GetAll().ToList();
            foreach (var item in ls)
            {
                string s = "Name: " + item.Name.ToString() + " Score: " + item.Score.ToString();
                this.lista.Items.Add(s);
            }
            */
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
