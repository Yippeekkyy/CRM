using MyCRM.Model;

namespace MyCRM.Responses;

public class GetWaitersResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int? Phone { get; set; }


    public GetWaitersResponse()
    {
        
    }

    public GetWaitersResponse(Waiter waiter)
    {
        Id = waiter.WaiterId;
        FirstName = waiter.FirstName;
        LastName = waiter.LastName;
        Patronymic = waiter.Patronimyc;
        Phone = waiter.Phone;
    }
}