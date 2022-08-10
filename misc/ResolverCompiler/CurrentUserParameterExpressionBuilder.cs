using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Internal;
using HotChocolate.Resolvers;
using HotChocolate.Types.Descriptors;

public class CurrentUserParameterExpressionBuilder
    : CustomParameterExpressionBuilder
    , IParameterFieldConfiguration
{
    private readonly Expression<Func<IResolverContext, User?>> _expression =
        c => c.GetGlobalValue<User>("currentUser");

    public void ApplyConfiguration(ParameterInfo parameter, ObjectFieldDescriptor descriptor)
    {
        descriptor.Use(next => async context =>
        {
            if (!context.ContextData.ContainsKey("currentUser") &&
                context.ContextData.TryGetValue("currentUserId", out var value) &&
                value is int currentUserId)
            {
                await using var dbContext =
                    await context.Service<IDbContextFactory<AssetContext>>().CreateDbContextAsync();
                User? user = await dbContext.Users.FindAsync(currentUserId);
                context.ContextData.TryAdd("currentUser", user);
            }
            await next(context);
        });
    }

    public override Expression Build(ParameterInfo parameter, Expression context)
        => Expression.Invoke(_expression, context);

    public override bool CanHandle(ParameterInfo parameter)
        => parameter.ParameterType == typeof(User);
}
