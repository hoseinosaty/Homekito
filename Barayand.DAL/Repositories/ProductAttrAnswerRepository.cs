using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.Common.Services;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace Barayand.DAL.Repositories
{
    public class ProductAttrAnswerRepository : GenericRepository<ProductAttributeModel>, IPublicMethodRepsoitory<ProductAttributeModel>, IProductAttrAnswerRepository
    {
        private readonly BarayandContext _context;
        public ProductAttrAnswerRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<IncomeProdFieldSetModel>> GetProductAttributes(int pid)
        {
            try
            {
                var prod = _context.Product.FirstOrDefault(x => x.P_Id == pid);
                if (prod == null)
                {
                    return null;
                }

                List<AttributeModel> attributes = _context.Attribute.AsNoTracking().Where(x => x.A_IsDeleted == false).ToList();
                List<CatAttrRelationModel> CategoryRelations = _context.CategoryAttribute.AsNoTracking().Where(x =>( x.X_CatId == prod.P_MainCatId || x.X_CatId == prod.P_EndLevelCatId) && !x.X_IsDeleted && x.X_Status ).ToList();
                List<ProductAttributeModel> AllAnswers = _context.ProductAttributeAnswer.AsNoTracking().ToList();
                List<AttrAnswerModel> DefAnswers = _context.AttributeAnswer.AsNoTracking().ToList();
                List<IncomeProdFieldSetModel> result = new List<IncomeProdFieldSetModel>();
                foreach (var catattr in CategoryRelations)
                {
                    IncomeProdFieldSetModel ipfs = new IncomeProdFieldSetModel();
                    var attr = attributes.FirstOrDefault(x => x.A_Id == catattr.X_AttrId);
                    if (attr != null)
                    {
                        ipfs.type = attr.A_Type;
                        ipfs.title = attr.A_Title;
                        var ans = AllAnswers.FirstOrDefault(x => x.X_AId == attr.A_Id && x.X_PId == pid);
                        ipfs.id = attr.A_Id;
                        ipfs.productid = pid;
                        if (ans != null)
                        {
                            if (ipfs.type == 2)
                            {
                                ipfs.value = ans.X_AnswerTitle;
                            }
                            else
                            {
                                ipfs.value = ans.X_AnswerId;
                            }
                        }
                        else
                        {
                            ipfs.value = null;
                        }
                        if(ipfs.type == 1)
                        {
                            var defans = DefAnswers.Where(x => x.X_CatAttrId == catattr.X_Id && x.X_IsDeleted == false && x.X_Status ==true).ToList() ;
                            ipfs.Answers = defans.Select(x=> new {value= x.X_Id,text = x.X_Answer});
                        }
                        result.Add(ipfs);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<IncomeProdFieldSetModel>();
            }
        }
       
        public async Task<ResponseStructure> SetProductAttributes(List<IncomeProdFieldSetModel> attrs)
        {
            try
            {
                List<ProductAttributeModel> AllAnswers = _context.ProductAttributeAnswer.ToList();

                foreach (var item in attrs)
                {
                    var ans = AllAnswers.FirstOrDefault(x=>x.X_PId == item.productid && x.X_AId == item.id);
                    int AnswerId = 0;
                    bool isInteger = int.TryParse(item.value + "", out AnswerId);
                    if (ans != null)
                    {
                        ans.X_AnswerId = (isInteger) ? AnswerId : 0;
                        ans.X_AnswerTitle = (!isInteger) ? item.value : null;
                        _context.ProductAttributeAnswer.Update(ans);
                        await this.CommitAllChanges();
                    }
                    else
                    {
                        ProductAttributeModel answer = new ProductAttributeModel();
                        answer.X_PId = item.productid;
                        answer.X_AnswerTitle = (!isInteger) ? item.value : null;
                        answer.X_AnswerId = (isInteger) ? AnswerId : 0;
                        answer.X_AId = item.id;
                        _context.ProductAttributeAnswer.Add(answer);
                        await this.CommitAllChanges();
                    }
                }
                await this.CommitAllChanges();
                return ResponseModel.Success("فیلد های اختصاصی محصول با موفقیت تغییر یافت");
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError(data:ex);
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

        public async Task<List<int>> GetProductsByAnswer(int ansid)
        {
            try
            {
                List<int> result = new List<int>();
                var resp = await this.GetAll();
                List<ProductAttributeModel> AllAttrs = (List<ProductAttributeModel>)resp.Data;
                AllAttrs = AllAttrs.Where(x => x.X_AnswerId == ansid).ToList();
                foreach(var item in AllAttrs)
                {
                    result.Add(item.X_PId);
                }
                return result;
            }
            catch(Exception ex)
            {
                return new List<int>();
            }
        }
    }
}
