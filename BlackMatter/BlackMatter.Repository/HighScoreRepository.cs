// <copyright file="HighScoreRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using BlackMatter.Model;
    using BlackMatter.Repository.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Highscore repo.
    /// </summary>
    public class HighScoreRepository : StorageRepository<Highscore>, IHighscoreRepository
    {
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreRepository"/> class.
        /// </summary>
        public HighScoreRepository()
            : base()
        {
            this.filename = "scores.json";
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\");
            }

            if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}"))
            {
                File.Create(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}").Close();
            }
        }

        /// <inheritdoc/>
        public IQueryable<Highscore> GetAll()
        {
            if (File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}") == string.Empty)
            {
                return new List<Highscore>().AsQueryable();
            }

            return JsonConvert.DeserializeObject<List<Highscore>>(File.ReadAllText(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}")).AsQueryable();
        }

        /// <inheritdoc/>
        public override void Insert(Highscore obj)
        {
            List<Highscore> list = this.GetAll().ToList();
            list.Add(obj);
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}", json);
        }
    }
}