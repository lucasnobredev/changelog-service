using ChangeLogService.Domain;
using ChangeLogService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeLogService.Repository
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

        public IList<PullRequestEvent> GetAll(string repositoryName, string labelName)
        {
            var builder = Builders<PullRequestEvent>.Filter;

            var repositoryNameFilter = repositoryName == null ? builder.Where(x => x.RepositoryName !=null ) : builder.Eq(x => x.RepositoryName, repositoryName);

            var labelNameFilter = labelName == null ? 
                builder.Where(x => x.Labels != null) :
                builder.ElemMatch(x => x.Labels, x => x.Name == labelName);
            
            return collection.Find(repositoryNameFilter & labelNameFilter).ToList();
        }
    }
}
