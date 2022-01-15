using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YURT_OTOMASYONU.Infrastructure.Web
{
    public class InMemoryCache : ICacheService
    {
        public T Get<T>(string cacheId, Func<T> getItemCallback, int minutesincache = 30) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item != null) return item;
            item = getItemCallback();
            if (item != null)
            {
                HttpRuntime.Cache.Insert(
                    cacheId, //key
                    item, //object item
                    null, //dependency
                          //System.Web.Caching.Cache.NoAbsoluteExpiration,//absolute expiration
                          //new TimeSpan(0, 0, minutesincache, 0) //sliding expiration
                    DateTime.Now.AddMinutes(minutesincache), //absolute expiration
                    System.Web.Caching.Cache.NoSlidingExpiration //sliding expiration
                    );
            }
            return item;
        }

        public T Refresh<T>(string cacheId, Func<T> getItemCallback, int minutesincache = 30) where T : class
        {
            HttpRuntime.Cache.Remove(cacheId);
            var item = getItemCallback();
            if (item != null)
            {
                HttpRuntime.Cache.Insert(
                    cacheId, //key
                    item, //object item
                    null, //dependency
                          //System.Web.Caching.Cache.NoAbsoluteExpiration,//absolute expiration
                          //new TimeSpan(0, 0, minutesincache, 0) //sliding expiration);
                    DateTime.Now.AddMinutes(minutesincache), //absolute expiration
                    System.Web.Caching.Cache.NoSlidingExpiration //sliding expiration
                    );
            }
            return item;
        }

        public void Remove(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }

        public void RemoveAllStartingWith(string cacheId)
        {
            var keys = new List<string>();
            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // delete every key from cache
            foreach (string t in keys)
            {
                if (t.StartsWith(cacheId))
                    HttpRuntime.Cache.Remove(t);
            }
        }
    }


}
