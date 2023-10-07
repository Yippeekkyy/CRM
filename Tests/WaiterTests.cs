using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCRM.Database;
using MyCRM.Requests;
using Shouldly;

namespace Tests;


[Collection("Crm")]
public class WaiterTests
{
    private readonly HttpClient _httpClient;
    private readonly MainDbContext _dbContext;

    public WaiterTests(CrmFixture crmFixture)
    {
        _dbContext = crmFixture.ServiceScope.GetService<MainDbContext>();
        _httpClient = crmFixture.CreateClient();
    }
    
    
    [Fact]
    public async void Test1()
    {
        var request = new AddWaiterRequest()
        {
            FirstName = "john",
            LastName = "smith",
            Patronimyc = "Alexandrovich",
            Phone = 123456789
        };
        await _httpClient.PostAsJsonAsync("api/Admin1/CreateWaiter",request);

        var waiter = await _dbContext.Waiters.FirstAsync();
        
        waiter.FirstName.ShouldBe(request.FirstName);
        waiter.LastName.ShouldBe(request.LastName);
        waiter.Patronimyc.ShouldBe(request.Patronimyc);
        waiter.Phone.ShouldBe(request.Phone);
    }
}