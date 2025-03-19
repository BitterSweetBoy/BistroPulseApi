using Microsoft.AspNetCore.Authentication;
using Module.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Interfaces
{
    public interface ISessionStoreRepository
    {
        Task<string> StoreAsync(AuthenticationTicket ticket, string userId, string ipAddress, string userAgent);
        Task<bool> RenewAsync(string key, AuthenticationTicket ticket);
        Task<AuthenticationTicket?> RetrieveAsync(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExpireSessionAsync(string key);
        Task<bool> RevokeSessionByAdminAsync(string key);
        Task<SessionTicket?> RetrieveSessionAsync(string key);
        Task<bool> UpdateSessionAsync(SessionTicket session);
        Task<bool> ExpireInactiveSessionsAsync();
    }
}
