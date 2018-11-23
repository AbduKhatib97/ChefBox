using ChefBox.Model.Base;
using ChefBox.SqlServer.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ChefBox.Cooking.Data.Repositories.Base
{
    public abstract class BaseCookingRepository<EntityType, TId> where EntityType : BaseEntity<TId>
    {
        protected ChefBoxDbContext Context { get; set; }

        protected BaseCookingRepository(ChefBoxDbContext context)
        {
            Context = context;
        }

        protected virtual EntityType GetSingleOrDefaultBaseEntity(TId id, bool? isValid)
        {
            return Context.Set<EntityType>().SingleOrDefault(
                entity =>
                entity.Id.Equals(id)
                &&
                (isValid.HasValue ? entity.IsValid.Equals(isValid) : true)
                );
        }

        protected virtual bool Remove(TId id)
        {
            try
            {
                var entity = GetSingleOrDefaultBaseEntity(id, isValid: true);

                if (entity is null) return false;

                entity.IsValid = false;
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual bool RemovePermanently(TId id)
        {
            try
            {
                var entity = GetSingleOrDefaultBaseEntity(id, isValid: true);

                if (entity is null) return false;

                Context.Entry(entity).State = EntityState.Deleted;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected virtual bool[] RemoveRangePermanently(params TId[] ids)
        {
            var results = new bool[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                results[i] = RemovePermanently(ids[i]);
            }

            return results;
        }
        protected virtual bool[] RemoveRange(params TId[] ids)
        {
            var results = new bool[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                results[i] = Remove(ids[i]);
            }

            return results;
        }

    }
}
