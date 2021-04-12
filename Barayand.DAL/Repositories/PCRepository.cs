using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;

using Microsoft.EntityFrameworkCore;

namespace Barayand.DAL.Repositories
{
    public class PCRepository : GenericRepository<ProductCategoryModel>,IPublicMethodRepsoitory<ProductCategoryModel>, IPCRepository
    {
        private readonly BarayandContext _context;
 
        public PCRepository(BarayandContext context):base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> Delete(ProductCategoryModel entity)
        {
            try
            {
                this._context.ProductCategory.Remove(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg:ex.Message);
            }
        }

        public async Task<ResponseStructure> Delete(object id)
        {
            try
            {
                var entity = await this.GetById(id);
                this._context.ProductCategory.Remove(entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message);
            }
        }

        public async Task Dispose()
        {
            try
            {
                await this._context.DisposeAsync();
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task<ResponseStructure> GetAll()
        {
            try
            {
                List<ProductCategoryModel> data = this._context.ProductCategory.Where(x=>x.PC_IsDeleted == false).ToList();
                foreach(var item in data)
                {
                    string ParentTitle = "****";
                    int lvl = 1;
                    if(item.PC_ParentId != 0)
                    {
                        var parent = await this.GetById(item.PC_ParentId);
                        ParentTitle = parent.PC_Title;
                        lvl = await this.GetCategoryLevel(item.PC_Id);
                    }
                    data.FirstOrDefault(x => x.PC_Id == item.PC_Id).PC_ParentTitle = ParentTitle;
                    data.FirstOrDefault(x => x.PC_Id == item.PC_Id).PC_Level = lvl;
                    if(lvl == Constants.MAX_CAT_LEVEL || lvl == (Constants.MAX_CAT_LEVEL - 1))
                    {
                        data.FirstOrDefault(x => x.PC_Id == item.PC_Id).PC_AttrAble = true;
                    }

                }
                data = data.OrderBy(x=>x.PC_Level).ToList();
               
                return ResponseModel.Success(data:data);
            }
            catch (Exception ex)
            {
                return  ResponseModel.Error(msg:ex.Message,data:ex);
            }
        }
     
        private async Task<ResponseStructure> GetAllInside()
        {
            try
            {
                List<ProductCategoryModel> data = await Task.Run(() => this._context.ProductCategory.ToList());
               
                return ResponseModel.Success(data: data);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message, data: ex);
            }
        }
        public async Task<ResponseStructure> GetAllByParentIdOneLevel(int parent)
        {
            try
            {
                List<ProductCategoryModel> data = await  this._context.ProductCategory.ToListAsync();
                var items = data.Where(x=>x.PC_ParentId == parent).ToList();
                return ResponseModel.Success(data: items);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message, data: ex);
            }
        }
        public List<ProductCategoryModel> GetAllPaged(int startindex, int count, out int totalcount)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategoryModel> GetById(object id)
        {
            try
            {
                var entity = await Task.Run(()=> this._context.ProductCategory.FirstOrDefault(x => x.PC_Id == int.Parse(id.ToString()))) ;
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> Insert(ProductCategoryModel entity)
        {
            try
            {
                int max = 0;
                if(entity.PC_Type == 1)
                {
                    max = Constants.MAX_CAT_LEVEL;
                }
                else if(entity.PC_Type == 2)
                {
                    max = Constants.MAX_DIGITAL_CAT_LEVEL;
                }
                else
                {
                    max = Constants.MAX_TRAINING_CAT_LEVEL;
                }
                if(entity.PC_ParentId != 0)
                {
                    var getAll = (List<ProductCategoryModel>)(await GetAll()).Data;
                    int maxLevel = getAll.FirstOrDefault(x =>x.PC_Id == entity.PC_ParentId).PC_Level;
                    if (maxLevel >= max)
                    {
                        return ResponseModel.Error(string.Format("تعداد سطوح دسته بندی نمیتواند بیشتر از {0} باشد",max.ToString()));
                    }
                }
                entity.Created_At = DateTime.Now;
                entity.Updated_At = DateTime.Now;
                await this._context.ProductCategory.AddAsync(entity:entity);
                await this.CommitAllChanges();
                return ResponseModel.Success("operation successfully completed");
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
                List<ProductCategoryModel> AllCats = (List<ProductCategoryModel>)(await this.GetAll()).Data;
                var Current = AllCats.FirstOrDefault(x=>x.PC_Id == (int)id);
                if(Current == null)
                {
                    return ResponseModel.Error("دسته بندی مورد نظر یافت نشد");
                }
                Current.PC_Status = newState;
                List<ProductCategoryModel> AllChilds = new List<ProductCategoryModel>();
                foreach(var item in GetAllChild((int)id, AllCats))
                {
                    item.PC_Status = newState;
                    AllChilds.Add(item);
                }
                AllChilds.Add(Current);
                return await this.UpdateRange(entities:AllChilds);
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
                List<ProductCategoryModel> AllCats = (List<ProductCategoryModel>)(await GetAllInside()).Data;
                var Current = AllCats.FirstOrDefault(x => x.PC_Id == (int)id);
                if (Current == null)
                {
                    return ResponseModel.Error("دسته بندی مورد نظر یافت نشد");
                }
                Current.PC_IsDeleted = true;
                List<ProductCategoryModel> AllChilds = new List<ProductCategoryModel>();
                foreach (var item in GetAllChild((int)id, AllCats))
                {
                    item.PC_IsDeleted = true;
                    AllChilds.Add(item);
                }
                AllChilds.Add(Current);
                return await this.UpdateRange(entities: AllChilds);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ProductCategoryModel> GetAllChild(int id, List<ProductCategoryModel> newLst)
        {
            List<ProductCategoryModel> list = new List<ProductCategoryModel>();
            //var t = new Stopwatch();
            //t.Start();
            for (int i = 0; i < newLst.Count; i++)
            {
                if (Convert.ToInt32(newLst[i].PC_ParentId) == id)
                {
                    if (!list.Contains(newLst[i]))
                    {
                        list.Add(newLst[i]);
                        List<ProductCategoryModel> l = GetAllChild(newLst[i].PC_Id, newLst);
                        list.AddRange(l);
                    }
                }
            }
            //t.Stop();
            return list;
        }
        public async Task<ResponseStructure> Update(ProductCategoryModel entity)
        {
            try
            {
                var item = this._context.ProductCategory.AsNoTracking().FirstOrDefault(x=>x.PC_Id == entity.PC_Id);
                entity.Created_At = item.Created_At;
                entity.Updated_At = DateTime.Now;
                this._context.ProductCategory.Update(entity);
                await this._context.SaveChangesAsync();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseStructure> UpdateRange(List<ProductCategoryModel> entities)
        {
            try
            {
                foreach(var item in entities)
                {
                    entities.FirstOrDefault(x=>x.PC_Id == item.PC_Id).Updated_At = DateTime.Now;
                }
                this._context.ProductCategory.UpdateRange(entities:entities);
                await this._context.SaveChangesAsync();
                return ResponseModel.Success("رکورد مورد نظر با موفقیت بروزرسانی گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> GetCategoryLevel(int cid)
        {
            try
            {
                List<ProductCategoryModel> All = (List<ProductCategoryModel>)(await this.GetAllInside()).Data;
                var item = All.FirstOrDefault(x=>x.PC_Id == cid);
                if (item.PC_ParentId == 0)
                    return 1;
                bool end = false;
                int level = 1;
                int nextlvl = item.PC_ParentId;
                while(!end)
                {
                    var eachItem = All.FirstOrDefault(x=>x.PC_Id == nextlvl);
                    level++;
                    if(eachItem.PC_ParentId != 0)
                    {
                        nextlvl = eachItem.PC_ParentId;
                    }
                    else
                    {
                        end = true;
                    }
                }
                return level;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<ProductCategoryModel>> GetCategoryParents(int cid)
        {
            try
            {
                List<ProductCategoryModel> All = (List<ProductCategoryModel>)(await this.GetAllInside()).Data;
                var item = All.FirstOrDefault(x => x.PC_Id == cid);
                if (item.PC_ParentId == 0)
                    return null;
                bool end = false;
                int level = 1;
                List<ProductCategoryModel> categories = new List<ProductCategoryModel>();
                int nextlvl = item.PC_ParentId;
                while (!end)
                {
                    var eachItem = All.FirstOrDefault(x => x.PC_Id == nextlvl);
                    categories.Add(eachItem);
                    if (eachItem.PC_ParentId != 0)
                    {
                        nextlvl = eachItem.PC_ParentId;
                    }
                    else
                    {
                        end = true;
                    }
                }
                return categories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseStructure> GetAllMenu()
        {
            try
            {
                List<ProductCategoryModel> data = this._context.ProductCategory.AsNoTracking().Where(x => x.PC_IsDeleted == false && x.PC_Status).ToList();
                return ResponseModel.Success(data: data);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: ex.Message, data: ex);
            }
        }
    }
}
