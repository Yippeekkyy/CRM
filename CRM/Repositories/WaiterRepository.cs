using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyCRM.Database;
using MyCRM.Model;

namespace MyCRM.Repositories;

public class WaiterRepository 
{
    //private static ConcurrentDictionary<int, Waiter>? waiterCache;

    private readonly MainDbContext _dbContext;

    public WaiterRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;

       // if (waiterCache is null)
        //{
        //    waiterCache = new ConcurrentDictionary<int, Waiter>(_dbContext.Waiter.ToDictionary(w => w.WaiterId));
        //}
    }


   public async Task Add(Waiter waiter)
    {
        await _dbContext.Waiter.AddAsync(waiter);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var waitertodelete = await _dbContext.Waiter.FindAsync(id);
        if (waitertodelete is not null)
        {
            _dbContext.Waiter.Remove(waitertodelete);
            await _dbContext.SaveChangesAsync();
        }
        
    }

    

    

    
    

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   /* public async Task<Waiter?> CreateAsync(Waiter w)
    {
        EntityEntry<Waiter> added = await _dbContext.Waiter.AddAsync(w);
        int affected = await _dbContext.SaveChangesAsync();
        if (affected == 1)
        {
            if (waiterCache is null) return w;
            return waiterCache.AddOrUpdate(w.WaiterId, w, UpdateCache);
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<Waiter>> RetrieveAllAsync()
    {
        return Task.FromResult(waiterCache is null ? Enumerable.Empty<Waiter>() : waiterCache.Values);
    }

    public Task<Waiter?> RetrieveAsync(int id)
    {
        if (waiterCache is null) return null!;
        waiterCache.TryGetValue(id, out Waiter? w);
        return Task.FromResult(w);
    }

    private Waiter UpdateCache(int id, Waiter w)
    {
        Waiter? old;
        if (waiterCache is not null)
        {
            if (waiterCache.TryGetValue(id, out old))
            {
                if (waiterCache.TryUpdate(id, w,old))
                {
                    return w;
                }
            }
        }

        return null!;
    }

    public async Task<Waiter?> UpdateAsync(int id, Waiter w)
    {
        //обновляем в базе
        _dbContext.Waiter.Update(w);
        int affected = await _dbContext.SaveChangesAsync();
        if (affected == 1)
        {
            //обновляем в кэше
            return UpdateCache(id, w);
        }

        return null;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        //Удаляем из базы данных
        Waiter? w = _dbContext.Waiter.Find(id);
        if (w is null) return null;
        _dbContext.Waiter.Remove(w);
        int affected = await _dbContext.SaveChangesAsync();
        if (affected == 1)
        {
            //Удаляем из кэша
            if (waiterCache is null) return null;
            return waiterCache.TryRemove(id, out w);
        }
        else
        {
            return null;
        }
    }*/
}