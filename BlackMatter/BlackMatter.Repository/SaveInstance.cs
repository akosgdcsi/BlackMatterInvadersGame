using BlackMatter.Model;
using BlackMatter.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Repository
{
    public class SaveInstance : StorageRepository<GameModel>, ISaveInstanceRepository
    {
        string filename;
        public SaveInstance()
        {
            this.filename = "savegame.json";
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\");
            }
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}"))
            {
                File.Create(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}").Close();
            }
        }
        public override void Insert(GameModel obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}", json);
        }

        public GameModel LoadGame()
        {
            if (File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}") == string.Empty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<GameModel>(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}"));
        }
    }
}
