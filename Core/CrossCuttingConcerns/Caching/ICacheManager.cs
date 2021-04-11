using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// Bir tipteki cache değerini okur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Bir tipteki cache değerini okur.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// Cache eklemek için kullanılır.Key/Data/Süre
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="duration"></param>
        void Add(string key, object data, int duration);

        /// <summary>
        /// Cache ten mi getirilecek yoksa DB den getirilerek Cache e mi eklenecek olduğunu döner.(Eklenmiş mi?)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsAdd(string key);

        /// <summary>
        /// Belli bir key deki cache in ortadan kaldırılmasını sağlar.
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Pattern e uyan cache lerin silinmesi istenebilir.(Get ile başlayan bütün Cache leri sil gibi.)
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);
    }
}
