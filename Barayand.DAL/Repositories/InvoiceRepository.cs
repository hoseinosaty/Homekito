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
    public class InvoiceRepository : GenericRepository<InvoiceModel>, IPublicMethodRepsoitory<InvoiceModel> 
    {
        private readonly BarayandContext _context;
        public InvoiceRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
        public async Task<InvoiceModel> GetById(object id)
        {
            try
            {
                return ((List<InvoiceModel>)((await this.GetAll()).Data)).FirstOrDefault(x => x.I_Id == id.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Update(InvoiceModel entity)
        {
            try
            {
                var item = await this.GetById(entity.I_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Invoice.Update(entity);
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
