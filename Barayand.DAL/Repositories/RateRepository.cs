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
    public class RateRepository : GenericRepository<RateModel>, IPublicMethodRepsoitory<RateModel>, IRateRepository
    {
        private readonly BarayandContext _context;

        public RateRepository(BarayandContext context) : base(context)
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
        public async Task<ResponseStructure> Insert(RateModel entity)
        {
            try
            {
                if(await ExistsRate(entity.R_Ip,entity.R_EntityId,entity.R_Type))
                {
                    return ResponseModel.Error("You have been rated this entity.");
                }
                await this._context.Rate.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("Thank you.your rate has been saved.");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> ExistsRate(string ip,int eid,int etype)
        {
            try
            {
                return ((List<RateModel>)((await this.GetAll()).Data)).Count(x => x.R_Ip == ip && x.R_EntityId == eid && x.R_Type == etype) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<decimal> CalulateRate(int entity, int type)
        {
            try
            {
                List<RateModel> rates = ((List<RateModel>)((await this.GetAll()).Data)).Where(x => x.R_EntityId == entity && x.R_Type == type).ToList() ;
                decimal x = Barayand.Common.Services.UtilesService.CalculateRate(rates.Count(x=>x.R_Rate == 5), rates.Count(x => x.R_Rate == 4), rates.Count(x => x.R_Rate == 3), rates.Count(x => x.R_Rate == 2), rates.Count(x => x.R_Rate == 1));
                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
