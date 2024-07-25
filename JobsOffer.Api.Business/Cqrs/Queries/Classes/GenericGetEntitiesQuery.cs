using JobsOffer.Api.Business.Cqrs.Queries.Interfaces;
using JobsOffer.Api.Business.Helpers;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Text.Json;

namespace JobsOffer.Api.Business.Cqrs.Queries.Classes
{
    public class GenericGetEntitiesQuery<TEntity> : IGenericGetEntitiesQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextJobsOffer> _unitOfWork;
        protected readonly IMemoryCache? _cache = null;
        protected readonly IDistributedCache? _distributedCache = null;
        protected string? CacheKey { get; set; }
        #endregion

        #region CONSTRUCTOR
        public GenericGetEntitiesQuery(
            IUnitOfWork<DbContextJobsOffer> unitOfWork,
            IMemoryCache? cache = null,
            IDistributedCache? distributedCache = null
            ) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
            if(cache != null) _cache = cache ?? throw new ArgumentException(null, nameof(cache));
            if(distributedCache != null) _distributedCache = distributedCache ?? throw new ArgumentException(null, nameof(distributedCache));
            if(cache != null) CacheKey = $"{typeof(TEntity).Name}Cache";
        }
        #endregion

        #region METHODS
        public IQueryable<TEntity> Handle(
            Expression<Func<TEntity, bool>>? expression = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null, 
            string? includes = null, 
            string splitChar = ",", 
            bool disableTracking = true, 
            int take = 0, 
            int offset = 0,
            bool inDatabase = false)
        {
            if (!inDatabase)
            {
                if (_cache is not null)
                {
                    if (_cache.TryGetValue(CacheKey!, out IList<TEntity>? cachedListData))
                    {
                        return GetEntities(cachedListData, expression, orberBy, includes ?? GetIncludes(), splitChar, disableTracking, take, offset);
                    }

                    if (_distributedCache is not null)
                    {
                        var serializedData = _distributedCache.GetString(CacheKey!);
                        if (!string.IsNullOrEmpty(serializedData))
                        {
                            var cachedDistributedListData = JsonSerializer.Deserialize<IList<TEntity>>(serializedData);
                            _cache.Set(CacheKey!, cachedDistributedListData);
                            return GetEntities(cachedDistributedListData, expression, orberBy, includes ?? GetIncludes(), splitChar, disableTracking, take, offset);
                        }
                    }

                    if(!_cache.TryGetValue(CacheKey!, out IList<TEntity>? cachedLocalListData))
                    {
                        var includesString = GetIncludes();
                        var cachedSettedListData = _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(expression: null, orberBy: null, includesString, splitChar, disableTracking, take: 0, offset: 0).ToList();
                        _cache.Set(CacheKey!, cachedSettedListData);
                        return GetEntities(cachedSettedListData, expression, orberBy, includes ?? includesString, splitChar, disableTracking, take, offset);
                    }
                }
            }
            return _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(expression, orberBy, includes ?? GetIncludes(), splitChar, disableTracking, take, offset);
        }

        public async Task<TEntity?> Handle(TEntity entity) => await _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(entity);

        public IQueryable<TEntity?> Handle()
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            if (!_cache.TryGetValue(CacheKey!, out IList<TEntity>? cachedLocalListData))
            {
                var includesString = GetIncludes();
                var cachedListData = _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(expression: null, orberBy: null, includesString, splitChar: ",", disableTracking: true, take: 0, offset: 0).ToList();
                _cache!.Set(CacheKey!, cachedListData);
                return GetEntities(cachedListData, expression: null, orberBy: null, includesString, splitChar: ",", disableTracking: true, take: 0, offset: 0);
            }
            return cachedLocalListData.AsQueryable();
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
        }
        #endregion

        #region HELPER
        private IQueryable<TEntity> GetEntities(
            IList<TEntity>? cachedListData,
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null,
            string? includes = null,
            string splitChar = ",",
            bool disableTracking = true,
            int take = 0,
            int offset = 0)
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            var cachedFilterData = cachedListData.AsQueryable() ?? new List<TEntity>().AsQueryable();
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
            if (cachedFilterData.Count() > 0)
            {
                if (expression != null)
                {
                    cachedFilterData = cachedFilterData.Where(expression);
                }

                if (orberBy != null)
                {
                    cachedFilterData = orberBy(cachedFilterData);
                }

                if (includes != null)
                {
                    foreach (var include in includes.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                    {
                        cachedFilterData = cachedFilterData.Include(include);
                    }
                }

                if (disableTracking)
                {
                    cachedFilterData = cachedFilterData.AsNoTracking();
                }

                if (take > 0)
                {
                    cachedFilterData = cachedFilterData.Take(take);
                }

                if (offset > 0)
                {
                    cachedFilterData = cachedFilterData.Skip(offset);
                }
            }
            return cachedFilterData ?? new List<TEntity>().AsQueryable();
        }

        private string GetIncludes()
        {
            var typesList = Helper.GetAttributeTypes(typeof(TEntity)).Where(x => x.TypeClass == TypeClass.Collection || x.TypeClass == TypeClass.Class).ToList();
            var includesList = new List<string>();
            typesList.ForEach(x => {
                var propertyTypeName = x.PropertyType.Name + (x.TypeClass == TypeClass.Collection ? "s" : string.Empty);
                includesList.Add(propertyTypeName); 
            });
            return string.Join(',', includesList.ToArray());
        }
        #endregion
    }
}
