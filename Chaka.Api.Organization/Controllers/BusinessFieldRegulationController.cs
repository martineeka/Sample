using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.BusinessFieldRegulation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class BusinessFieldRegulationController : ControllerBase
    {
        IBusinessFieldRegulationServices _BussinessFieldProvider;
        IMapper _mapper;

        public BusinessFieldRegulationController(IBusinessFieldRegulationServices BussinessFieldProvider, IMapper mapper)
        {
            _BussinessFieldProvider = BussinessFieldProvider;
            _mapper = mapper;
        }

        #region Field
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _BussinessFieldProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = levels.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var model = new CreateEditViewModel();
            var enitity = _BussinessFieldProvider.Get(Id);
            if (enitity == null)
            {
                return NotFound("Data not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(enitity, model);
            return Ok(enitity);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            var enitity = new BusinessFieldRegulation();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, enitity);
            _BussinessFieldProvider.Add(enitity);
            return Ok(enitity);
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var jobfamily = _BussinessFieldProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, jobfamily);
            _BussinessFieldProvider.Edit(jobfamily);
            return Ok();
        }

        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            var model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    var ValidateDelete = _BussinessFieldProvider.ValidateDelete(ID);
                    if (ValidateDelete.IsSuccess)
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on " + ValidateDelete.Message + " menu";

                        return BadRequest(model);
                    }
                }
                Array.ForEach(listID, _BussinessFieldProvider.Delete);
                model.IsSuccess = true;
                model.Message = "Delete Successfully";
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("ValidateCode/{code}/{id}")]
        public IActionResult ValidateCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isCodeValid = _BussinessFieldProvider.ValidCode(code, decryptID);
            return Ok(isCodeValid);
        }

        [HttpGet]
        [Route("ValidateCodeExist/{code}")]
        public IActionResult ValidateCodeExist(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isCodeExist = _BussinessFieldProvider.ExistCode(code);
            return Ok(isCodeExist);
        }
        #endregion Field
       
        #region Category
        [HttpPost]
        [Route("GetCategory/{BusinessFieldRegulationID}")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request, string BusinessFieldRegulationID)
        {
            try
            {
                var lists = _BussinessFieldProvider.GetListCategory(Convert.ToInt32(BusinessFieldRegulationID));

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = lists.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("GetCategory/{Id}")]
        public IActionResult GetCategory(int Id)
        {
            var model = new CreateEditCategoryViewModel();
            var enitity = _BussinessFieldProvider.GetCategory(Id);
            if (enitity == null)
            {
                return NotFound("Data not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(enitity, model);
            return Ok(enitity);
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory([FromBody] CreateEditCategoryViewModel model)
        {
            var enitity = new BusinessFieldCategory();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, enitity);
            _BussinessFieldProvider.AddCategory(enitity);
            return Ok(enitity);
        }

        [HttpPut]
        [Route("EditCategory")]
        public IActionResult EditCategory([FromBody] CreateEditCategoryViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var entity = _BussinessFieldProvider.GetCategory(Convert.ToInt32(model.ID));
            _mapper.Map(model, entity);
            _BussinessFieldProvider.EditCategory(entity);
            return Ok();
        }

        [HttpPost]
        [Route("DeleteCategory/{arrayOfID}")]
        public IActionResult DeleteCategory(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            var model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    var ValidateDelete = _BussinessFieldProvider.ValidateDeleteCategory(ID);
                    if (ValidateDelete.IsSuccess)
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on " + ValidateDelete.Message + " menu";

                        return BadRequest(model);
                    }
                }
                Array.ForEach(listID, _BussinessFieldProvider.DeleteCategory);
                model.IsSuccess = true;
                model.Message = "Delete Succesfully";
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("ValidateCodeCategory/{code}/{id}")]
        public IActionResult ValidateCodeCategory(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isCodeValid = _BussinessFieldProvider.ValidCodeCategory(code, decryptID);
            return Ok(isCodeValid);
        }

        [HttpGet]
        [Route("ValidateCodeExistCategory/{code}")]
        public IActionResult ValidateCodeExistCategory(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isCodeExist = _BussinessFieldProvider.ExistCodeCategory(code);
            return Ok(isCodeExist);
        }

        [HttpGet]
        [Route("GetWithRegulationName/{Id}")]
        public IActionResult GetWithName(int Id)
        {
            var model = new CreateEditCategoryViewModel();
            var enitity = _BussinessFieldProvider.GetWithRegulationName(Id);
            if (enitity == null)
            {
                return NotFound("Data not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(enitity, model);
            return Ok(enitity);
        }
        #endregion Category

        #region Classification
        [HttpPost]
        [Route("GetClassification/{BusinessFieldCategoryID}")]
        public IActionResult GetClassification([FromBody]CustomDataSourceRequest request, string BusinessFieldCategoryID)
        {
            try
            {
                var lists = _BussinessFieldProvider.GetListClassification(Convert.ToInt32(BusinessFieldCategoryID));

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = lists.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("GetClassification/{Id}")]
        public IActionResult GetClassification(int Id)
        {
            var model = new CreateEditClassificationViewModel();
            var enitity = _BussinessFieldProvider.GetClassification(Id);
            if (enitity == null)
            {
                return NotFound("Data not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(enitity, model);
            return Ok(enitity);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddClassification")]
        public IActionResult PostClassification([FromBody] CreateEditClassificationViewModel model)
        {
            var enitity = new BusinessFieldClassification();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, enitity);
            _BussinessFieldProvider.AddClassification(enitity);
            return Ok(enitity);
        }

        [HttpPut]
        [Route("EditClassification")]
        public IActionResult PutClassification([FromBody] CreateEditClassificationViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var entity = _BussinessFieldProvider.GetClassification(Convert.ToInt32(model.ID));
            _mapper.Map(model, entity);
            _BussinessFieldProvider.EditClassification(entity);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("DeleteClassification/{arrayOfID}")]
        public IActionResult DeleteClassification(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            var model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    var ValidateDelete = _BussinessFieldProvider.ValidateDeleteClassification(ID);
                    if (ValidateDelete.IsSuccess)
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on " + ValidateDelete.Message + " menu";

                        return BadRequest(model);
                    }
                }
                Array.ForEach(listID, _BussinessFieldProvider.DeleteClassification);
                model.IsSuccess = true;
                model.Message = "Delete Successfully";
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("ValidateCodeClassification")]
        public IActionResult ValidateCodeClassification([FromBody] CreateEditClassificationViewModel model)
        {
            var enitity = new BusinessFieldClassification();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, enitity);
            var isCodeExist = _BussinessFieldProvider.ValidCodeClassification(enitity);
            return Ok(isCodeExist);
        }

        [HttpPost]
        [Route("ValidateCodeExistClassification")]
        public IActionResult ValidateCodeExist([FromBody] CreateEditClassificationViewModel model)
        {
            var enitity = new BusinessFieldClassification();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, enitity);
            var isCodeExist = _BussinessFieldProvider.ExistCodeClassification(enitity);
            return Ok(isCodeExist);
        }
        #endregion Classfication
    }
}