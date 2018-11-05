using ChefBox.Model.Base;
using ChefBox.SqlServer.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefBox.Cooking.Data.Repositories.Base
{
    public class BaseRepository
    {
        public ChefBoxDbContext Context { get; set; }
        public BaseRepository(ChefBoxDbContext context)
        {
            Context = context;
        }
        protected EntityType GetSingleOrDefaultBaseEntity<EntityType>(int id, bool? isValid) where EntityType : BaseEntity<int>
        {
            return Context.Set<EntityType>()
                          .SingleOrDefault(entity => entity.Id == id
                                                     &&
                                                     (isValid.HasValue ? entity.IsValid == isValid : true)
                                          );
        }
    }
}

