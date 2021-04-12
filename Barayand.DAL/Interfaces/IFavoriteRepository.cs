using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IFavoriteRepository : IPublicMethodRepsoitory<FavoriteModel>
    {
        Task<bool> ChekExistsInList(int entity,int user,int type = 1);
        Task<List<FavoriteList>> GetByUser(int user);
    }
}
