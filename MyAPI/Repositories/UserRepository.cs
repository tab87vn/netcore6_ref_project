public class UserRepository : IUSerRepository
{
    public string TestUser(User user) => user.ToString();
}