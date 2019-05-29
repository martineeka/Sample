using System;
using System.Linq;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Chaka.ViewModels.Organization.Level;
using Newtonsoft.Json;
using Chaka.Database.Context.Models;
using Microsoft.AspNetCore.Authorization;

namespace Chaka.Api.Organization.Controllers
{
   
    [Route("api/Organization/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelProvider;
        private readonly ILevelCategoryService _levelCategoryProvider;
        private IMapper _mapper;


        public LevelController(ILevelService levelProvider,
                                ILevelCategoryService levelCategoryProvider,
                                IMapper mapper)
        {
            _levelProvider = levelProvider;
            _levelCategoryProvider = levelCategoryProvider;
            _mapper = mapper;
        }


        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _levelProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = levels.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception)
            {

                throw;
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var model = new CreateEditViewModel();
            var level = _levelProvider.Get(Id);
            if (level == null)
            {
                return NotFound("Level not found.");
            }

            model.ID = Id.ToString();
            model.MaxSequenceLevel = _levelProvider.GetMaxSequenceLevel();
            var levelMapper = _mapper.Map(level, model);
            return Ok(levelMapper);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            var level = new Level();
            if (model is null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var levelMapper = _mapper.Map(model, level);
            _levelProvider.Add(levelMapper);
            return Ok(levelMapper);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var level = _levelProvider.Get(Convert.ToInt32(model.ID));
            
            var levelMapper = _mapper.Map(model, level);
            _levelProvider.Edit(levelMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            try
            {
                string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
                int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
                Array.ForEach(listID, _levelProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("DeleteRow/{Id}")]
        public IActionResult DeleteRow(string Id)
        {
            try
            {
                var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(Id));
                _levelProvider.Delete(decryptID);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("MaxSequenceLevel")]
        public IActionResult GetMaxSequenceLevel()
        {
            var model = new CreateEditViewModel();
            model.MaxSequenceLevel = _levelProvider.GetMaxSequenceLevel();
            return Ok(model.MaxSequenceLevel);
        }

        [HttpGet]
        [Route("CategoryLevel")]
        public IActionResult GetBusinessGroup()
        {
            var data = _levelCategoryProvider.GetList();
            return Ok(data.Select(x => new { Value = x.ID, Text = x.Name }));
        }

        [HttpGet]
        [Route("ValidateLevelCode/{code}/{id}")]
        public IActionResult ValidateLevelCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _levelProvider.IsLevelCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _levelProvider.validateCombination(model);
            return Ok(isValid);
        }


    }
}