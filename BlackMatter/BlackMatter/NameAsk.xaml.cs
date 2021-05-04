// <copyright file="NameAsk.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for NameAsk.xaml.
    /// </summary>
    public partial class NameAsk : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameAsk"/> class.
        /// </summary>
        public NameAsk()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameControl.PlayerName = this.Davuu.Text;
            this.Close();
        }
    }
}