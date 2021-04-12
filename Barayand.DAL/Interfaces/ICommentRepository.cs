using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface ICommentRepository : IPublicMethodRepsoitory<CommentModel>
    {
        Task<List<CommentModel>> AcceptedComments(int entityId,int entityType);
    }
}
