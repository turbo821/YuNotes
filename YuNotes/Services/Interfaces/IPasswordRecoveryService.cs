namespace YuNotes.Services.Interfaces
{
    public interface IPasswordRecoveryService
    {
        Task<string> SendCodeAsync(string email);
    }
}
