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
    public class NewsLetterRepository : GenericRepository<NewsletterModel>, IPublicMethodRepsoitory<NewsletterModel>
    {
        private readonly BarayandContext _context;

        public NewsLetterRepository(BarayandContext context) : base(context)
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
        public async Task<ResponseStructure> Insert(NewsletterModel entity)
        {
            try
            {
                var all = (List<NewsletterModel>)(await this.GetAll()).Data;
                if(all.Count(x=>x.NL_Entity == entity.NL_Entity) > 0)
                {
                    return ResponseModel.Error("This email address has already subscribed.");
                }
                await this._context.NewsLetter.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("Your subscription has been confirmed.You've been added to our list and will hear from us soon.");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
