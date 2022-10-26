using System.Reflection;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PISWF;

public abstract class AppRepository<T> where T : class
{
    public Func<int> SaveChanges { get; }
    
    public DbSet<T>? Entity { get; }

    public AppRepository(AppDbContext appDbContext)
    {
        var predicate = new Func<PropertyInfo, bool>(x => x.GetMemberType().Equals(typeof(DbSet<T>)));
        Entity = appDbContext
            .GetType()
            .GetProperties()
            .Where(predicate)
            .FirstOrDefault()
            ?.GetValue(appDbContext) 
            as DbSet<T>;
        SaveChanges = appDbContext.SaveChanges;
    }
    
    public void UpdateAndSave(T entity)
    {
        Entity.Update(entity);
    }
}