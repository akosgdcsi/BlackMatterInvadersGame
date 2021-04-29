// <copyright file="StorageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository
{
    using BlackMatter.Repository.Interfaces;

    /// <summary>
    /// storagerepo class.
    /// </summary>
    /// <typeparam name="T">is calss.</typeparam>
    public abstract class StorageRepository<T> : IStorageRepository<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageRepository{T}"/> class.
        /// </summary>
        public StorageRepository()
        {
        }

        /// <inheritdoc/>
        public abstract void Insert(T obj);
    }
}