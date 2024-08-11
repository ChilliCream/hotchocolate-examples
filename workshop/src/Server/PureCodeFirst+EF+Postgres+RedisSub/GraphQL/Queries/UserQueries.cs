using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using SlackClone.Models;
using HotChocolate.AspNetCore.Authorization;

namespace SlackClone.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        //[Authorize]
        [UseSelection, UseFirstOrDefault]
        public IQueryable<User> Me(
                [GlobalState]string currentUserEmail,
                [Service] SlackCloneDbContext dbContext) =>
            dbContext.Users.Where(user => user.Email == currentUserEmail);

        /// <summary>
        /// Gets a user.
        /// </summary>
        // [Authorize]
        [UseSelection, UseFiltering, UseSorting]
        public IQueryable<User> Users(
                [Service] SlackCloneDbContext dbContext) =>
            dbContext.Users;
    }
}