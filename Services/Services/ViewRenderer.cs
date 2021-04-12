using Barayand.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Services
{
    public class ViewRenderer : IViewRenderer
    {
        private readonly IRazorViewEngine viewEngine;

        public ViewRenderer(IRazorViewEngine viewEngine) => this.viewEngine = viewEngine;

        public async Task<string> RenderAsync<TModel>(Controller controller, string name, TModel model)
        {
            ViewEngineResult viewEngineResult = this.viewEngine.FindView(controller.ControllerContext, name, false);

            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException(string.Format("Could not find view: {0}", name));
            }

            IView view = viewEngineResult.View;
            controller.ViewData.Model = model;

            await using var writer = new StringWriter();
            var viewContext = new ViewContext(
               controller.ControllerContext,
               view,
               controller.ViewData,
               controller.TempData,
               writer,
               new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return writer.ToString();
        }
    }
}
