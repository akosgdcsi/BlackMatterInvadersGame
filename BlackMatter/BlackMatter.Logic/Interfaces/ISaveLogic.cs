// <copyright file="ISaveLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Logic.Interfaces
{
    /// <summary>
    /// Interface Savelogic.
    /// </summary>
    public interface ISaveLogic
    {
        /// <summary>
        /// Saves game model status.
        /// </summary>
        void SaveInstance();

        /// <summary>
        /// loads the game.
        /// </summary>
        void LoadGame();

        /// <summary>
        /// saves Highscore.
        /// </summary>
        void HighscoreInstance();

        /// <summary>
        /// Gets all highscore.
        /// </summary>
        void LoadHighscore();
    }
}