using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IFileAccessService
    {
        Task<bool> UserAccessProductFile(int pid,int userid);
    }
}
