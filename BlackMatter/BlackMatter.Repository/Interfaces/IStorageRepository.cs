// <copyright file="IStorageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository.Interfaces
{
    /// <summary>
    /// interface of storagerepository.
    /// </summary>
    /// <typeparam name="T">is a class.</typeparam>
    public interface IStorageRepository<T>
        where T : class
    {
        /// <summary>
        /// adds to the file.
        /// </summary>
        /// <param name="obj">sends a class object.</param>
        void Insert(T obj);
    }
}