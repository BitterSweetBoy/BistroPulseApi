using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Shared.Interfaces
{
    public interface ISessionValidator
    {
        Task<bool> ValidateSessionAsync();
    }
}
