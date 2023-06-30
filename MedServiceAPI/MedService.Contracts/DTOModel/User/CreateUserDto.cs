
namespace MedService.Contracts.DTOModel.User
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
}
