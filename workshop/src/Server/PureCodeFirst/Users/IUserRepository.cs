using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task AddUserAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task UpdatePasswordAsync(
            string email,
            string newPasswordHash,
            string salt,
            CancellationToken cancellationToken = default);
    }
}