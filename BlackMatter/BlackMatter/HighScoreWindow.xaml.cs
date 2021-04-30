// <copyright file="HighScoreWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using BlackMatter.Model;
    using BlackMatter.Repository;

    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml.
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreWindow"/> class.
        /// </summary>
        public HighScoreWindow()
        {
            this.InitializeComponent();

            this.FillListBox();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillListBox()
        {
            HighScoreRepository highScore = new HighScoreRepository();
            List<Highscore> ls = highScore.GetAll().ToList();
            foreach (var item in ls)
            {
                string s = "Name: " + item.Name.ToString() + " Score: " + item.Score.ToString();
                this.lista.Items.Add(s);
            }
        }
    }
}