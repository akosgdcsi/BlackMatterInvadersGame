using BlackMatter.Model;
using BlackMatter.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackMatter.Repository
{
    public abstract class StorageRepository<T> : IStorageRepository<T> where T : class
    {
        public StorageRepository()
        {

        }

        public abstract void Insert(T obj);
        
    }
}
