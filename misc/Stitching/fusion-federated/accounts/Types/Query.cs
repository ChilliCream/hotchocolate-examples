namespace Demo.Accounts.Types;

[QueryType]
public static class Query
{
    public static IEnumerable<User> GetUsers(UserRepository repository) 
        => repository.GetUsers();

    [NodeResolver]
    public static User? GetUserById(int id, [Service] UserRepository repository) =>
        repository.GetUser(id);

    public static IEnumerable<User> GetUsersById(
        [ID<User>] IEnumerable<int> ids,
        [Service] UserRepository repository)
    {
        foreach (var id in ids)
        {
            var user = repository.GetUser(id);

            if (user is not null)
            {
                yield return user;
            }
        }
    }
}
