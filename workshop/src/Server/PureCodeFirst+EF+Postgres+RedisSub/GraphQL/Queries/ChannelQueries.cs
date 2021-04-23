using System;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using SlackClone.Models;
using HotChocolate.AspNetCore.Authorization;

namespace SlackClone.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class ChannelQueries
    {
        /// <summary>
        /// Gets all channels.
        /// </summary>
        [Authorize]
        [UseSorting]
        [UseFiltering]
        [UseSelection]
        public IQueryable<Channel> Channels([Service] SlackCloneDbContext dbContext)
        {
            return dbContext.Channels;
        }

        /// <summary>
        /// Gets all channel messages.
        /// </summary>
        [Authorize]
        [UseSelection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<ChannelMessage> ChannelMessages([Service] SlackCloneDbContext dbContext)
        {
            return dbContext.ChannelMessages;
        }
    }
}
