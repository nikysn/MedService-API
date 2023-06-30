namespace MedServiceAPI.Model.Requests.User
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
