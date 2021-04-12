using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface ILogService
    {
        Task LogException(Exception ex);
        Task LogResult(object res);
    }
}
