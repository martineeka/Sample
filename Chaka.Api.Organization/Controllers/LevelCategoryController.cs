using System;
using System.Linq;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.LevelCategory;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    public class LevelCategoryController : ControllerBase
    {

        private readonly ILevelCategoryService _levelCategoryProvider;
        private IMapper _mapper;

        public LevelCategoryController(ILevelCategoryService levelCategoryProvider, IMapper mapper)
        {
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
                var levels = _levelCategoryProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = levels.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
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
            var level = _levelCategoryProvider.Get(Id);
            if (level == null)
            {
                return NotFound("Level Category not found.");
            }

            model.ID = Id.ToString();
            model.MaxSequenceLevel = _levelCategoryProvider.GetMaxSequenceLevel();
            var levelMapper = _mapper.Map(level, model);
            return Ok(levelMapper);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            var level = new LevelCategory();
            if (model is null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var levelMapper = _mapper.Map(model, level);
            _levelCategoryProvider.Add(levelMapper);
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
            var level = _levelCategoryProvider.Get(Convert.ToInt32(model.ID));
            var levelMapper = _mapper.Map(model, level);
            _levelCategoryProvider.Edit(levelMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _levelCategoryProvider.Delete);
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
            model.MaxSequenceLevel = _levelCategoryProvider.GetMaxSequenceLevel();
            return Ok(model.MaxSequenceLevel);
        }

        [HttpGet]
        [Route("ValidateLevelCategoryCode/{code}/{id}")]
        public IActionResult ValidateLevelCategoryCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _levelCategoryProvider.IsLevelCategoryCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpGet]
        [Route("ValidateLevelCategoryCode/{code}")]
        public IActionResult ValidateLevelCategoryCode(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _levelCategoryProvider.IsLevelCategoryCodeValidNotId(code);
            return Ok(isLevelCodeValid);
        }
    }
}