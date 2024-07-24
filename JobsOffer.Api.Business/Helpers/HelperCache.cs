using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.Extensions.Caching.Memory;

namespace JobsOffer.Api.Business.Helpers
{
    public class HelperCache<TEntity> where TEntity : Entity
    {
        public static void AddCache(TEntity entity, IMemoryCache cache)
        {
            if (entity is null) return;
            var cacheKey = $"{typeof(TEntity).Name}Cache";
            var cachedListData = cache.GetOrCreate(cacheKey, entry => new List<TEntity>());
            if (cachedListData is not null)
            {
                cachedListData.Add(entity);
                cache.Set(cacheKey, cachedListData);
            }
        }


        public static void AddCache(IList<TEntity> entities, IMemoryCache cache)
        {
            if (entities is null || !entities.Any()) return;
            var cacheKey = $"{typeof(TEntity).Name}Cache";
            var cachedListData = cache.GetOrCreate(cacheKey, entry => new List<TEntity>());
            if (cachedListData is not null)
            {
                cachedListData.AddRange(entities);
                cache.Set(cacheKey, cachedListData);
            }
        }


        public static void DeleteCache(TEntity entity, IMemoryCache cache)
        {
            if (entity is null) return;
            var cacheKey = $"{typeof(TEntity).Name}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            if (cache.TryGetValue(cacheKey, out IList<TEntity> cachedListData))
            {
                var itemToRemove = cachedListData?.FirstOrDefault(x => x.Equals(entity));
                if (itemToRemove is not null)
                {
                    cachedListData?.Remove(itemToRemove);
                    cache.Set(cacheKey, cachedListData);
                }
            }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
        }


        public static void DeleteCache(IList<TEntity> entities, IMemoryCache cache)
        {
            if (entities is null || !entities.Any()) return;
            var cacheKey = $"{typeof(TEntity).Name}Cache";
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            if (cache.TryGetValue(cacheKey, out IList<TEntity> cachedListData))
            {
                if (cachedListData is not null)
                {
                    foreach (var entity in entities)
                    {
                        var itemToRemove = cachedListData.FirstOrDefault(x => x.Equals(entity));
                        if (itemToRemove is not null)
                        {
                            cachedListData.Remove(itemToRemove);
                        }
                    }
                    cache.Set(cacheKey, cachedListData);
                }
            }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
        }


    }
}
