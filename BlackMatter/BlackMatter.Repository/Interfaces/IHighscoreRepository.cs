// <copyright file="IHighscoreRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository.Interfaces
{
    using System.Linq;
    using BlackMatter.Model;

    /// <summary>
    /// interface for Highscore.
    /// </summary>
    public interface IHighscoreRepository : IStorageRepository<Highscore>
    {
        /// <summary>
        /// gets all the highscore.
        /// </summary>
        /// <returns>IQueryable-Highscore.</returns>
        IQueryable<Highscore> GetAll();
    }
}