using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Shared.Response
{
    public record ApiResponse<T>(
        bool Success,
        int StatusCode,
        T Data,
        Dictionary<string, string[]> Errors = null
    );
}
