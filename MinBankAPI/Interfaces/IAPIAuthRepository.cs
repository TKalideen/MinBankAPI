namespace MinBankAPI.Interfaces
{
    public interface IAPIAuthRepository
    {
        string Login(string username, string password);
    }
}
