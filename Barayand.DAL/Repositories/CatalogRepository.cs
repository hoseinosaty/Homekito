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
    public class CatalogRepository : GenericRepository<CatalogModel>, IPublicMethodRepsoitory<CatalogModel>
    {
        private readonly BarayandContext _context;
        public CatalogRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                var All = this._context.Catalog.ToList();
                List<CatalogModel> result = new List<CatalogModel>();
                foreach (var item in All)
                {
                    var cat = this._context.GalleryCategory.FirstOrDefault(x => x.GC_Type == 3 && x.GC_Id == item.C_CatId);
                    if (cat != null)
                    {
                        item.C_CatTitle = cat.GC_Titles;
                        result.Add(item);
                    }
                }
                return ResponseModel.Success(data: result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            try
            {
                var item = await this.GetById(id);
                item.C_Status = newState;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> LogicalDelete(object id)
        {
            try
            {
                var item = await this.GetById(id);
                //item.isd = true;
                return await this.Update(item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> Update(CatalogModel entity)
        {
            try
            {
                var item = await this.GetById(entity.C_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                this._context.Catalog.Update(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
