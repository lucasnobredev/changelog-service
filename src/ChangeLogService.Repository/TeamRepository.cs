using ChangeLogWeb.Domain;
using ChangeLogWeb.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeLogWeb.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<Team> collection { get; set; }

        public TeamRepository()
        {
            Client = new MongoClient("mongodb://localhost");
            Database = Client.GetDatabase("changelog-web");
            collection = Database.GetCollection<Team>("team");
        }

        public void Insert(Team team)
        {
            collection.InsertOne(team);
        }

        public IList<Team> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }

        public Team GetByKeys(string majorTeam, string childTeam = null)
        {
            var builder = Builders<Team>.Filter;

            var majorTeamFilter = builder.Where(x => x.Name == majorTeam);
            var labelNameFilter = childTeam != null ? 
                builder.Where(x => x.ChildrenTeams.Any(y => y.Name == childTeam)) :
                builder.Empty;

            return collection
                .Find(majorTeamFilter & labelNameFilter)
                .FirstOrDefault();
        }
    }
}
