using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DesafioBackend.Infrastructure.Config;
using DesafioBackend.Core.Models;

namespace DesafioBackend.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            Console.WriteLine($"Conexão MongoDB - ConnectionString: {mongoDbSettings.Value.ConnectionString}, DatabaseName: {mongoDbSettings.Value.DatabaseName}");
            string conn = "mongodb://localhost:27017/";
            string db = "DesafioBackendDB";
            
            var client = new MongoClient(conn);
            _database = client.GetDatabase(db);

        }

         public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
