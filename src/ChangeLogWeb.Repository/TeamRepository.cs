using ChangeLogWeb.Domain;
using ChangeLogWeb.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
            team.Id = ObjectId.GenerateNewId();
            if (team.ChildrenTeams != null)
            {
                foreach (var child in team.ChildrenTeams)
                    child.Id = ObjectId.GenerateNewId();
            }
            collection.InsertOne(team);
        }

        public IList<Team> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }
    }
}
