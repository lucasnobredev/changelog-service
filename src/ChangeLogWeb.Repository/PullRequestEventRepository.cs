using ChangeLogWeb.Domain;
using ChangeLogWeb.Domain.Interfaces;
using MongoDB.Driver;
using System;

namespace ChangeLogWeb.Repository
{
    public class PullRequestEventRepository : IPullRequestEventRepository
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }

        public PullRequestEventRepository()
        {
            Client = new MongoClient("mongodb://localhost");
            Database = Client.GetDatabase("changelog-web");
        }

        public void Insert(PullRequestEvent pullRequestEvent)
        {
            var collection = Database.GetCollection<PullRequestEvent>("pullrequest_event");
            collection.InsertOne(pullRequestEvent);
        }
    }
}
