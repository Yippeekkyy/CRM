namespace MyCRM.Requests
{
    public class EditWaiterRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? Phone { get; set; }
    }
}
