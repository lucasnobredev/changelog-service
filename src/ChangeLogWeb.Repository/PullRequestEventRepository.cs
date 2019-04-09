using ChangeLogWeb.Domain;
using ChangeLogWeb.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ChangeLogWeb.Repository
{
    public class PullRequestEventRepository : IPullRequestEventRepository
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<PullRequestEvent> collection { get; set; }

        public PullRequestEventRepository()
        {
            Client = new MongoClient("mongodb://localhost");
            Database = Client.GetDatabase("changelog-web");
            collection = Database.GetCollection<PullRequestEvent>("pullrequest_event");
        }

        public void Insert(PullRequestEvent pullRequestEvent)
        {
            collection.InsertOne(pullRequestEvent);
        }

        public IList<PullRequestEvent> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }
    }
}
