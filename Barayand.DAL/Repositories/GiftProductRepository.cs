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
    public class GiftProductRepository : GenericRepository<GiftProductModel>, IGiftProductRepository
    {
        private readonly BarayandContext _context;
        public GiftProductRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> GetAllRelation(Miscellaneous data)
        {
            try
            {
                var allRelations = this._context.GiftProduct.Where(x => x.X_MainProdId == data.Id).ToList();
                return ResponseModel.Success(data: allRelations);
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }

        public async Task<ResponseStructure> UpdateRelation(List<GiftProductModel> data,int prodid)
        {
            try
            {
                if (data.Count() > 1)
                {
                    return ResponseModel.Error("به هر محصول فقط میتوان یک هدیه نسبت داد");
                }

                var rel = this._context.GiftProduct.ToList();
                this._context.GiftProduct.RemoveRange(rel.Where(x => x.X_MainProdId == prodid).ToList());
                await this.CommitAllChanges();
                
                if (data == null || data.Count() == 1)
                {
                    await this._context.GiftProduct.AddRangeAsync(data);
                    await this.CommitAllChanges();
                }
                
                return ResponseModel.Success("عملیات با موفقیت انجام گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
    }
}
