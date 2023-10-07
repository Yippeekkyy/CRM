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


    }
        
}


    

    

    
    

    
    
    
    
    
    
    
    
    
    
    
    
    
    
  
