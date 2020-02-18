namespace ExpertalSystem.Authorization
{
    public interface IJwtManager
    {
        string GenerateToken(string id, string username, int expireMinutes = 20);
    }
}
