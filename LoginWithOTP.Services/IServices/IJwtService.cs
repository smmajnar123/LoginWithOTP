namespace LoginWithOTP.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string mobileNumber);
    }
}
