using BlackMatter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Repository.Interfaces
{
    public interface IHighscoreRepository : IStorageRepository<Highscore>
    {
        IQueryable<Highscore> GetAll();
    }
}
