using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barayand.Common.Constants;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barayand.Controllers.BaseSetting
{
    [Route("api/cpanel/basesetting/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IPublicMethodRepsoitory<HeaderNotificationModel> _headernotifyrepo;
        private readonly IPublicMethodRepsoitory<FaqCategoryModel> _faqcatrepo;
        private readonly IPublicMethodRepsoitory<FaqModel> _faqrepo;
        private readonly IPublicMethodRepsoitory<Province> _provincerepo;
        private readonly IPublicMethodRepsoitory<States> _staterepo;
        private readonly IMapper _mapper;
        public BaseController(IPublicMethodRepsoitory<Province> provincerepo, IPublicMethodRepsoitory<States> staterepo, IPublicMethodRepsoitory<HeaderNotificationModel> headernotifyrepo, IPublicMethodRepsoitory<FaqCategoryModel> faqcatrepo, IMapper mapper, IPublicMethodRepsoitory<FaqModel> faqrepo)
        {
            _provincerepo = provincerepo;
            _staterepo = staterepo;
            _headernotifyrepo = headernotifyrepo;
            _faqcatrepo = faqcatrepo;
            _faqrepo = faqrepo;
            _mapper = mapper;
        }
        #region Header Notification
        [Route("AddHeaderNotify")]
        [HttpPost]
        public async Task<IActionResult> AddHeaderNotify(HeaderNotificationModel hnm)
        {
            try
            {
                return new JsonResult(await _headernotifyrepo.Insert(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateHeaderNotify")]
        [HttpPost]
        public async Task<IActionResult> UpdateHeaderNotify(HeaderNotificationModel hnm)
        {
            try
            {
                return new JsonResult(await _headernotifyrepo.Update(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DisableHeaderNotify")]
        [HttpPost]
        public async Task<IActionResult> DisableHeaderNotify(HeaderNotificationModel hnm)
        {
            try
            {
                return new JsonResult(await _headernotifyrepo.LogicalAvailable(hnm.H_Id, false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveHeaderNotify")]
        [HttpPost]
        public async Task<IActionResult> ActiveHeaderNotify(HeaderNotificationModel hnm)
        {
            try
            {
                return new JsonResult(await _headernotifyrepo.LogicalAvailable(hnm.H_Id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadHeaderNotifies")]
        [HttpPost]
        public async Task<IActionResult> LoadHeaderNotifies()
        {
            try
            {
                var allNotifies = ((List<HeaderNotificationModel>)(await _headernotifyrepo.GetAll()).Data);
                return new JsonResult(ResponseModel.Success(data: allNotifies));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }

        #endregion
        #region Faq Category
        [Route("AddFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> AddFaqCategory(FaqCategoryModel hnm)
        {
            try
            {
                return new JsonResult(await _faqcatrepo.Insert(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> UpdateFaqCategory(FaqCategoryModel hnm)
        {
            try
            {
                return new JsonResult(await _faqcatrepo.Update(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> DeleteFaqCategory(FaqCategoryModel hnm)
        {
            try
            {
                return new JsonResult(await _faqcatrepo.LogicalDelete(hnm.F_Id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }        
        [Route("DisableFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> DisableFaqCategory(FaqCategoryModel hnm)
        {
            try
            {
                return new JsonResult(await _faqcatrepo.LogicalAvailable(hnm.F_Id, false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> ActiveFaqCategory(FaqCategoryModel hnm)
        {
            try
            {
                return new JsonResult(await _faqcatrepo.LogicalAvailable(hnm.F_Id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadFaqCategory")]
        [HttpPost]
        public async Task<IActionResult> LoadFaqCategory()
        {
            try
            {
                var allNotifies = ((List<FaqCategoryModel>)(await _faqcatrepo.GetAll()).Data).Where(x => x.F_IsDeleted == false).ToList();
                return new JsonResult(ResponseModel.Success(data: allNotifies));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadFaqCategoryCombo")]
        [HttpPost]
        public async Task<IActionResult> LoadFaqCategoryComboItems()
        {
            try
            {
                var allNotifies = ((List<FaqCategoryModel>)(await _faqcatrepo.GetAll()).Data).Where(x => x.F_IsDeleted == false && x.F_Status).ToList();
                List<ComboItems.FaqCategory> ComboItems = _mapper.Map<List<FaqCategoryModel>, List<ComboItems.FaqCategory>>(allNotifies);
                return new JsonResult(ResponseModel.Success(data: ComboItems));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        #endregion
        #region Faq 
        [Route("AddFaq")]
        [HttpPost]
        public async Task<IActionResult> AddFaq(FaqModel hnm)
        {
            try
            {
                return new JsonResult(await _faqrepo.Insert(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("UpdateFaq")]
        [HttpPost]
        public async Task<IActionResult> UpdateFaq(FaqModel hnm)
        {
            try
            {
                return new JsonResult(await _faqrepo.Update(hnm));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("DeleteFaq")]
        [HttpPost]
        public async Task<IActionResult> DeleteFaq(FaqModel hnm)
        {
            try
            {
                return new JsonResult(await _faqrepo.LogicalDelete(hnm.FA_Id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }        
        [Route("DisableFaq")]
        [HttpPost]
        public async Task<IActionResult> DisableFaq(FaqModel hnm)
        {
            try
            {
                return new JsonResult(await _faqrepo.LogicalAvailable(hnm.FA_Id, false));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("ActiveFaq")]
        [HttpPost]
        public async Task<IActionResult> ActiveFaq(FaqModel hnm)
        {
            try
            {
                return new JsonResult(await _faqrepo.LogicalAvailable(hnm.FA_Id, true));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        [Route("LoadFaq")]
        [HttpPost]
        public async Task<IActionResult> LoadFaq()
        {
            try
            {
                var allNotifies = ((List<FaqModel>)(await _faqrepo.GetAll()).Data).Where(x => x.FA_IsDeleted == false).ToList();
                return new JsonResult(ResponseModel.Success(data: allNotifies));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.ServerInternalError(data: ex));
            }
        }
        #endregion
        /// <summary>
        /// Get Max Level Count For Deny User - Product Category Creation Page
        /// </summary>
        /// <returns></returns>
        [Route("GetMaxLevel/{type?}")]
        [HttpPost]
        public async Task<ActionResult> GetMaxLevel(int type = 1)//Default Physical
        {
            try
            {
                type = type == 0 ? 1 : type;
                int MAX = 0;
                if (type == 1)
                {
                    MAX = Constants.MAX_CAT_LEVEL;
                }
                else if (type == 2)
                {
                    MAX = Constants.MAX_DIGITAL_CAT_LEVEL;
                }
                else if (type == 3)
                {
                    MAX = Constants.MAX_TRAINING_CAT_LEVEL;
                }
                return new JsonResult(ResponseModel.Success(data:MAX));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadProvinces")]
        [HttpPost]
        public async Task<ActionResult> LoadProvinces()
        {
            try
            {
                return new JsonResult(await _provincerepo.GetAll());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("LoadStates/{prov?}")]
        [HttpPost]
        public async Task<ActionResult> LoadStates(int prov = 0)
        {
            try
            {
                var AllStates = ((List<States>)(await _staterepo.GetAll()).Data);
                if(prov != 0)
                {
                    AllStates = AllStates.Where(x=>x.Province == prov).ToList();
                }
                return new JsonResult(ResponseModel.Success(data:AllStates));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}