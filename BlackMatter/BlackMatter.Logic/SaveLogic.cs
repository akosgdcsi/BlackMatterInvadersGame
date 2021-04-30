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
        /// Initializes a new instance of the <see cref="SaveLogic"/> class.
        /// </summary>
        /// <param name="save">init save.</param>
        public SaveLogic(SaveInstance save)
        {
            this.save = save;
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
        /// <returns>game model.</returns>
        public GameModel LoadGame()
        {
            return this.save.LoadGame();
        }

        /// <summary>
        /// saves Highscore.
        /// </summary>
        /// <param name="name">init name.</param>
        public void HighscoreInstance(string name)
        {
            Highscore hs = new Highscore(name, this.model.Score);
            this.highScore.Insert(hs);
        }

        /// <summary>
        /// Gets all highscore.
        /// </summary>
        public void LoadHighscore()
        {
            this.highScore.GetAll();
        }

        /// <summary>
        /// Deletes Save.
        /// </summary>
        public void DeleteSave()
        {
            this.save.DeleteSave();
        }
    }
}