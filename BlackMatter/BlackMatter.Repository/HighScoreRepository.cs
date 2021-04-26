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
    public class HighScoreRepository : StorageRepository<Highscore>, IHighscoreRepository
    {
        string filename;
        public HighScoreRepository() : base()
        {
            this.filename = "scores.json";
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\");
            }
            if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}"))
            {
                File.Create(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}").Close();
            }
        }
        public IQueryable<Highscore> GetAll()
        {
            if (File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}") == string.Empty)
            {
                return new List<Highscore>().AsQueryable();
            }
            return JsonConvert.DeserializeObject<List<Highscore>>(File.ReadAllText(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}")).AsQueryable();
        }

        public override void Insert(Highscore obj)
        {
            List<Highscore> list = GetAll().ToList();
            list.Add(obj);
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\Saves\{filename}", json);
        }
    }
}
