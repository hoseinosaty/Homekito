using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class ProductManualRepository:GenericRepository<ProductManualModel>,IPublicMethodRepsoitory<ProductManualModel>,IProductManualRepository
    {
        private readonly BarayandContext _context;

        public ProductManualRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ProductManualModel> GetByProductId(int pid)
        {
            try
            {
                var AllManuals = ((List<ProductManualModel>)(await GetAll()).Data);
                return AllManuals.FirstOrDefault(x => x.M_ProductId == pid);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Insert(ProductManualModel entity)
        {
            try
            {
                var exists = _context.ProductManual.FirstOrDefault(x=>x.M_ProductId == entity.M_ProductId);
                if (exists != null)
                {
                    exists.M_FileName = entity.M_FileName;
                    exists.M_Status = entity.M_Status;
                    exists.M_Title = entity.M_Title;
                    exists.M_Price = entity.M_Price;
                    exists.Updated_At = DateTime.Now;
                    this._context.ProductManual.Update(entity: entity);
                    await this.CommitAllChanges();

                    return ResponseModel.Success();
                }
                entity.Created_At = DateTime.Now;
                entity.Updated_At = DateTime.Now;
                await this._context.ProductManual.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
