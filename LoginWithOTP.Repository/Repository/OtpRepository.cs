using LoginWithOTP.DbLayer.Collections;
using LoginWithOTP.DbLayer.DbContexts;
using LoginWithOTP.DbLayer.Models;
using LoginWithOTP.Repository.IRepository;
using MongoDB.Driver;

namespace LoginWithOTP.Repository.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly IMongoCollection<OtpRecordDocument> _collection;

        public OtpRepository(MongoDbContext context, MongoDbSettings settings)
        {
            var collectionName = settings.Collections["OtpRecords"];
            _collection = context.GetCollection<OtpRecordDocument>(collectionName);
        }

        public async Task<OtpRecordDocument?> GetLatestOtpAsync(string mobileNumber)
        {
            return await _collection
           .Find(x => x.MobileNumber == mobileNumber)
           .SortByDescending(x => x.CreatedAt)
           .FirstOrDefaultAsync();
        }

        public async Task SaveOtpAsync(OtpRecordDocument otp)
        {
            await _collection.InsertOneAsync(otp);
        }

        public async Task UpdateOtpAsync(OtpRecordDocument otp)
        {
            await _collection.ReplaceOneAsync(x => x.Id == otp.Id, otp);
        }
        public async Task<int> GetRecentOtpCountAsync(string mobileNumber, DateTime fromTime)
        {
            return (int)await _collection.CountDocumentsAsync(x =>
                x.MobileNumber == mobileNumber &&
                x.CreatedAt >= fromTime);
        }
    }
}
