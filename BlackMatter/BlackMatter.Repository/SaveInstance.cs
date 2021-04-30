// <copyright file="SaveInstance.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Repository
{
    using System.IO;
    using System.Reflection;
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;
    using BlackMatter.Repository.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// saveinstance class.
    /// </summary>
    public class SaveInstance : StorageRepository<IGameModel>, ISaveInstanceRepository
    {
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveInstance"/> class.
        /// </summary>
        public SaveInstance()
        {
            this.filename = "savegame.json";
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
        public override void Insert(IGameModel obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}", json);
        }

        /// <inheritdoc/>
        public GameModel LoadGame()
        {
            if (File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}") == string.Empty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<GameModel>(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}"));
        }

        /// <summary>
        /// Deletes Savefile.
        /// </summary>
        public void DeleteSave()
        {
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{this.filename}");
        }
    }
}