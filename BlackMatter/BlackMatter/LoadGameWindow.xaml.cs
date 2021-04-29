// <copyright file="LoadGameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter
{
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

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}