// <copyright file="ISaveInstanceRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository.Interfaces
{
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;

    /// <summary>
    /// interface if SaveInstanceRepository.
    /// </summary>
    public interface ISaveInstanceRepository : IStorageRepository<IGameModel>
    {
        /// <summary>
        /// loads a game.
        /// </summary>
        /// <returns>a game model.</returns>
        GameModel LoadGame();
    }
}