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
    public class FavoriteRepository : GenericRepository<FavoriteModel>, IPublicMethodRepsoitory<FavoriteModel>,IFavoriteRepository
    {
        private readonly BarayandContext _context;

        public FavoriteRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> ChekExistsInList(int entity, int user,int type = 1)
        {
            try
            {
                List<FavoriteModel> All = (List<FavoriteModel>)(await this.GetAll()).Data;
                return All.Count(x=>x.F_EntityId == entity && x.F_UserId == user && x.F_EntityType == type) > 0;
            }
            catch(Exception ex)
            {
                return true;
            }
        }
        public async Task<ResponseStructure> Insert(FavoriteModel entity)
        {
            try
            {
                if (await this.ChekExistsInList(entity.F_EntityId,entity.F_UserId,entity.F_EntityType))
                {
                    return ResponseModel.Error("Entity already exists in your favorite list.");
                }
                entity.Created_At = DateTime.Now;
                entity.Updated_At = DateTime.Now;
                await this._context.Favorites.AddAsync(entity: entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("Registration completed Successfully!");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError(data: ex);
            }
        }
        public async Task<List<FavoriteList>> GetByUser(int user)
        {
            try
            {
                List<FavoriteList> result = new List<FavoriteList>();
                List<FavoriteModel> all = this._context.Favorites.Where(x=>x.F_UserId == user).ToList();
                foreach(var item in all)
                {
                    switch(item.F_EntityType)
                    {
                        case 1:
                            var prd = this._context.Product.FirstOrDefault(x=>x.P_Id == item.F_EntityId && x.P_Type == 1);
                            if(prd != null)
                            {
                                result.Add(new FavoriteList() { 
                                Title = prd.P_Title,
                                Price = Barayand.Common.Services.UtilesService.FormatCurrency(prd.FinalPrice()),
                                Id = item.F_Id,
                                Image = Barayand.Common.Services.UtilesService.MediaUrls("ProductMainImage")+prd.P_Image,
                                Url = "/Product/Details/"+prd.P_Code+"/"+prd.P_Title,
                                Type=1
                                });
                            }
                            break;
                        case 2:
                            var prdd = this._context.Product.FirstOrDefault(x => x.P_Id == item.F_EntityId && x.P_Type == 2);
                            if (prdd != null)
                            {
                                result.Add(new FavoriteList()
                                {
                                    Title = prdd.P_Title,
                                    Price = Barayand.Common.Services.UtilesService.FormatCurrency(prdd.FinalPrice()),
                                    Id = item.F_Id,
                                    Image = Barayand.Common.Services.UtilesService.MediaUrls("ProductMainImage") + prdd.P_Image,
                                    Url = "/DigitalProducts/Details/" + prdd.P_Code + "/" + prdd.P_Title,
                                    Type = 2
                                });
                            }
                            break;
                        case 3:
                            var train = this._context.Trainings.FirstOrDefault(x => x.T_Id == item.F_EntityId );
                            if (train != null)
                            {
                                result.Add(new FavoriteList()
                                {
                                    Title = train.T_Title,
                                    Price = Barayand.Common.Services.UtilesService.FormatCurrency(decimal.Parse(train.T_Cost.ToString())),
                                    Id = item.F_Id,
                                    Image = Barayand.Common.Services.UtilesService.MediaUrls("ProductMainImage") + train.T_Image,
                                    Url = "/Training/Details/" + train.T_Code + "/" + train.T_Title,
                                    Type = 3
                                });
                            }
                            break;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                return new List<FavoriteList>();
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
