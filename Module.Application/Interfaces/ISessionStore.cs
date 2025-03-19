using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Interfaces
{
    public interface ISessionStore
    {
        Task<string> StoreAsync(AuthenticationTicket ticket, string userId, string ipAddress, string userAgent);
        Task RenewAsync(string key, AuthenticationTicket ticket);
        Task<AuthenticationTicket?> RetrieveAsync(string key);
        Task<bool> ExpireSessionAsync(string key);
        Task<bool> RevokeSessionByAdminAsync(string key);
        Task<bool> ValidateSessionAsync();
    }
}
