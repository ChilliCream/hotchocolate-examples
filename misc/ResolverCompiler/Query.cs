namespace Demo.Types;

public class Query
{
    public IQueryable<Asset> GetAssets(AssetContext context, User? currentUser)
    {
        if (currentUser is null)
        {
            return context.Assets.Take(2);
        }
        return context.Assets;
    }

    public string? CurrentUserName(User? user) => user?.Name;

    [UseSuperUser]
    public string? CurrentUserName2(User? user) => user?.Name;
}
