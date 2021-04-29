// <copyright file="SaveLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Logic
{
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;
    using BlackMatter.Repository;

    /// <summary>
    /// Save file.
    /// </summary>
    public class SaveLogic
    {
        private HighScoreRepository highScore;
        private SaveInstance save;
        private IGameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLogic"/> class.
        /// </summary>
        /// <param name="save">init save.</param>
        /// <param name="model">init model.</param>
        public SaveLogic(SaveInstance save, IGameModel model)
        {
            this.save = save;
            this.model = model;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLogic"/> class.
        /// </summary>
        /// <param name="highScore">init highscore.</param>
        /// <param name="model">init model.</param>
        public SaveLogic(HighScoreRepository highScore, IGameModel model)
        {
            this.highScore = highScore;
            this.model = model;
        }

        /// <summary>
        /// Saves game model status.
        /// </summary>
        public void SaveInstance()
        {
            this.save.Insert(this.model);
        }

        /// <summary>
        /// loads the game.
        /// </summary>
        public void LoadGame()
        {
            this.save.LoadGame();
        }

        /// <summary>
        /// saves Highscore.
        /// </summary>
        public void HighscoreInstance()
        {
            Highscore hs = new Highscore("Dave", this.model.Score);
            this.highScore.Insert(hs);
        }

        /// <summary>
        /// Gets all highscore.
        /// </summary>
        public void LoadHighscore()
        {
            this.highScore.GetAll();
        }
    }
}