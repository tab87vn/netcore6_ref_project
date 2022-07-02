using tab.TestDotNet.API.Models;

namespace tab.TestDotNet.API.Repositories;
public class UserRepository : IUSerRepository
{
    public string TestUser(User user) => user.ToString();
}