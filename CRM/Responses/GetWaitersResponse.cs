using MyCRM.Model;

namespace MyCRM.Responses;

public class GetWaiterResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int? Phone { get; set; }


    public GetWaiterResponse()
    {
        
    }

    public GetWaiterResponse(Waiter waiter)
    {
        Id = waiter.Id;
        FirstName = waiter.FirstName;
        LastName = waiter.LastName;
        Patronymic = waiter.Patronymic;
        Phone = waiter.Phone;
    }
}