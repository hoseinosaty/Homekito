using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IViewRenderer
    {
        Task<string> RenderAsync<TModel>(Microsoft.AspNetCore.Mvc.Controller controller, string name, TModel model);
    }
}
