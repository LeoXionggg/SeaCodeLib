using System.Runtime.Caching; 
using System;
using System.Collections.Generic;

namespace SeaCodeLib.Common
{
    public class InMemoryCache : ICacheHelper, IDisposable
    {
        MemoryCache memoryCache = MemoryCache.Default;
        private static List<string> Keys = new List<string>();
        public void Clear(string key)
        {
            memoryCache.Remove(key);
            if (Keys.Contains(key))
            {
                Keys.Remove(key);
            }
        }

        public void ClearALL()
        {
            Keys.ForEach(key => memoryCache.Remove(key));

            Keys.Clear();
        }

        public T Get<T>(string key, Func<T> acquire, int cacheTime = int.MaxValue)
        {
            T result;
            if (Keys.Contains(key))
            {
                result = Get<T>(key);
                if (result == null)
                {
                    result = acquire();
                    Set(key, result, cacheTime);
                }
            }
            else
            {
                result = acquire();
                //if (result != null)
                Set(key, result, cacheTime);

            }

            return result;
        }

        public T Get<T>(string key)
        {
            return (T)memoryCache.Get(key);
        }

        public void Set<T>(string key, T t)
        {
            memoryCache.Set(key, t, DateTimeOffset.MaxValue);
            if (!Keys.Contains(key))
            {
                Keys.Add(key);
            }
        }

        public void Set<T>(string key, T t, double timeoutseconds)
        {
            memoryCache.Set(key, t, new DateTimeOffset(DateTime.Now.AddSeconds(timeoutseconds)));
            if (!Keys.Contains(key))
            {
                Keys.Add(key);
            }
        }

        public void Dispose()
        {
            memoryCache.Dispose();
        }
    }
}
