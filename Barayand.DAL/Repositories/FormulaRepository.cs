using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.Common.Services;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Barayand.DAL.Repositories
{
    public class FormulaRepository : GenericRepository<FormulaModel>, IPublicMethodRepsoitory<FormulaModel>, IFormulaRepository
    {
        private readonly BarayandContext _context;

        public FormulaRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var formul = await this.GetById(id);
                formul.F_Status = newState;
                return await this.Update(formul);
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var formul = await this.GetById(id);
                formul.F_IsDeleted = true;
                return await this.Update(formul);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
