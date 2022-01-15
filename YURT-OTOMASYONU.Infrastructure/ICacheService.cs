using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURT_OTOMASYONU.Infrastructure
{
    public interface ICacheService
    {
        T Get<T>(string cacheId, Func<T> getItemCallback, int minutesincache = 30) where T : class;
        T Refresh<T>(string cacheId, Func<T> getItemCallback, int minutesincache = 30) where T : class;
        void Remove(string cacheId);
        void RemoveAllStartingWith(string cacheId);
    }
}
