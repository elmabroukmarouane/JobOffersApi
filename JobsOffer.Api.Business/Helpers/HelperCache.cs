using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsOffer.Api.Business.Helpers
{
    public class HelperCache
    {
        public static void AddCache(object entity, string typeEntity, IMemoryCache cache)
        {
            if (entity != null)
            {
                var cacheKey = $"{typeEntity}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                if (cache.TryGetValue(cacheKey, out IList<object> cachedListData))
                {
                    if (cachedListData != null)
                    {
                        cachedListData.Add(entity);
                        cache.Remove(cacheKey);
                        cache.Set(cacheKey, cachedListData);
                    }
                }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            }
        }

        public static void AddCache(IList<object> entities, string typeEntity, IMemoryCache cache)
        {
            if (entities != null && entities.Any() && entities.Count > 0)
            {
                var cacheKey = $"{typeEntity}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                if (cache.TryGetValue(cacheKey, out List<object> cachedListData))
                {
                    if (cachedListData != null)
                    {
                        cachedListData.AddRange(entities);
                        cache.Remove(cacheKey);
                        cache.Set(cacheKey, cachedListData);
                    }
                }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            }
        }

        public static void DeleteCache(object entity, string typeEntity, IMemoryCache cache)
        {
            if (entity != null)
            {
                var cacheKey = $"{typeEntity}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                if (cache.TryGetValue(cacheKey, out IList<object> cachedListData))
                {
                    if (cachedListData != null)
                    {
                        cachedListData.Remove(entity);
                        cache.Remove(cacheKey);
                        cache.Set(cacheKey, cachedListData);
                    }
                }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            }
        }

        public static void DeleteCache(IList<object> entities, string typeEntity, IMemoryCache cache)
        {
            if (entities != null && entities.Any() && entities.Count > 0)
            {
                var cacheKey = $"{typeEntity}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                if (cache.TryGetValue(cacheKey, out IList<object> cachedListData))
                {
                    if (cachedListData != null)
                    {
                        foreach (var entity in entities)
                        {
                            cachedListData.Remove(entity);
                        }
                        cache.Remove(cacheKey);
                        cache.Set(cacheKey, cachedListData);
                    }
                }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            }
        }
    }
}
