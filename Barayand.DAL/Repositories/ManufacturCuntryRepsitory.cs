using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class ManufacturCuntryRepsitory: GenericRepository<ManufacturContryModel>, IPublicMethodRepsoitory<ManufacturContryModel>
    {
        private readonly BarayandContext _context;

        public ManufacturCuntryRepsitory(BarayandContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.M_Status = newState;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var item = await this.GetById(id);
                item.M_Status = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<ResponseStructure> Update(ManufacturContryModel entity)
        {
            try
            {
                var item = await this.GetById(entity.M_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.ManufacturingCuntry.Update(entity);
                await this._context.SaveChangesAsync();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
