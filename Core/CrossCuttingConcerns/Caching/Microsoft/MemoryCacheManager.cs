using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private IMemoryCache _cache;

        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        /// <summary>
        /// "out _" değer döndürmeyi bekler ancak var/yok kontrolü yapar.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Cache collectionundaki elemanlara ulaşabilmek için (.NetCore da memory cache teki tüm elemanlara direk erişmek mümkün değil) kendi yapımızı(kod bloğumuzu) kuracağız.
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;

            //ICacheEntry her bir cache girişi.
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            //collectiondaki her bir değeri okuyarak,tek tek cacheCollectionValues içerisine atılır.
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            //string olarak gönderilen pattern e göre regex oluşturulur.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //cacheCollectionValues değerine göre regex değeri filtrelenir.
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            //gönderilen pattern deki cache ler silinmiş oldu.
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }
    }
}
