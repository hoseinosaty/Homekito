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
    public class CommentRepository : GenericRepository<CommentModel>, IPublicMethodRepsoitory<CommentModel>, ICommentRepository
    {
        private readonly BarayandContext _context;

        public CommentRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<CommentModel>> AcceptedComments(int entityId, int entityType)
        {
            try
            {
                var result = new List<CommentModel>();
                result = _context.Comment.Where(x => x.C_EntityId == entityId && x.C_Type == entityType && x.C_Status == 2).Take(20).ToList();
                return result;
            }
            catch(Exception ex)
            {
                return new List<CommentModel>();
            }
        }

        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var item = await this.GetById(id);
                if(item != null)
                {
                    _context.Comment.Remove(item);
                    await this.CommitAllChanges();
                }
                return ResponseModel.Success("کامنت مورد نظر با موفقیت حذف گردید");
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
            }
        }
        public async Task<ResponseStructure> Update(CommentModel entity)
        {
            try
            {
                var item = await this.GetById(entity.C_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Comment.Update(entity);
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
