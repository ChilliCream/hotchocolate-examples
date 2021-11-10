using System;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types; 
using HotChocolate.Types.Relay; 
using MongoDB.Driver;

namespace MongoDB
{
    public class Query
    {
        [UseMongoDbPaging]
        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IExecutable<Person> GetPersons([Service] IMongoCollection<Person> collection)
        {
            return collection.AsExecutable();
        }

        [UseFirstOrDefault]
        public IExecutable<Person> GetPersonById(
            [Service] IMongoCollection<Person> collection, 
            [ID]Guid id) 
        {
            return collection.Find(x => x.Id == id).AsExecutable();
        }
    }
}