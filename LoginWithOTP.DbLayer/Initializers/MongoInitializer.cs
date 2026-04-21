using LoginWithOTP.DbLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LoginWithOTP.DbLayer.Initializers
{
    public class MongoInitializer(IMongoClient client, MongoDbSettings settings)
    {
        private readonly IMongoDatabase _database = client.GetDatabase(settings.DatabaseName);
        private readonly MongoDbSettings _settings = settings;

        public async Task InitializeAsync()
        {
            try
            {
                var collections = await _database.ListCollectionNames().ToListAsync();
                if (!collections.Contains(_settings.Collections["Users"]))
                {
                    await _database.CreateCollectionAsync(_settings.Collections["Users"]);
                }
                if (!collections.Contains(_settings.Collections["OtpRecords"]))
                {
                    await _database.CreateCollectionAsync(_settings.Collections["OtpRecords"]);
                }
                var otpCollection = _database.GetCollection<BsonDocument>(_settings.Collections["OtpRecords"]);
                var indexKeys = Builders<BsonDocument>.IndexKeys
                    .Ascending("mobileNumber")
                    .Descending("createdAt");

                await otpCollection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(indexKeys));
                var ttlIndex = new CreateIndexModel<BsonDocument>(
                    Builders<BsonDocument>.IndexKeys.Ascending("expiryTime"),
                    new CreateIndexOptions { ExpireAfter = TimeSpan.Zero });

                await otpCollection.Indexes.CreateOneAsync(ttlIndex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB Initialization Error: {ex.Message}");
            }
        }
    }
}