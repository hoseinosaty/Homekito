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
    public class OrderRepository : GenericRepository<OrderModel>, IPublicMethodRepsoitory<OrderModel>
    {
        private readonly BarayandContext _context;
        public OrderRepository(BarayandContext context) : base(context)
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
        public async Task<ResponseStructure> Insert(OrderModel entity)
        {
            try
            {
                await this._context.Order.AddAsync(entity: entity);
                this._context.SaveChanges();
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Delete(object id)
        {
            try
            {
                List<OrderModel> Orders = ((List<OrderModel>)(await this.GetAll()).Data).Where(x=>x.O_ReciptId == id).ToList();
                
                this._context.Order.RemoveRange(Orders);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message);
            }
        }
    }
}
