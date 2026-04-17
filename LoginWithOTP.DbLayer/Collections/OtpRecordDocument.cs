using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.DbLayer.Collections
{
    public class OtpRecordDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("mobileNumber")]
        public required string MobileNumber { get; set; }

        [BsonElement("otpCode")]
        public required string OtpCode { get; set; }

        [BsonElement("expiryTime")]
        public DateTime ExpiryTime { get; set; }

        [BsonElement("isUsed")]
        public bool IsUsed { get; set; }

        [BsonElement("attemptCount")]
        public int AttemptCount { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        // 🔐 Security fields
        [BsonElement("deviceId")]
        public string? DeviceId { get; set; }

        [BsonElement("ipAddress")]
        public string? IpAddress { get; set; }

        // 🔄 Optional
        [BsonElement("otpType")]
        public string? OtpType { get; set; }
    }
}
