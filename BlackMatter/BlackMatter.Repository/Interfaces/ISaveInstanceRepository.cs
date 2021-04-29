// <copyright file="ISaveInstanceRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository.Interfaces
{
    using BlackMatter.Model;

    /// <summary>
    /// interface if SaveInstanceRepository.
    /// </summary>
    public interface ISaveInstanceRepository : IStorageRepository<GameModel>
    {
        /// <summary>
        /// loads a game.
        /// </summary>
        /// <returns>a game model.</returns>
        GameModel LoadGame();
    }
}