using System;
using System.Linq;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.GroupGrade;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    public class GroupGradeController : ControllerBase
    {
        private readonly IGroupGradeService _groupGradeProvider;
        private IMapper _mapper;


        public GroupGradeController(IGroupGradeService groupGradeProvider,
                                 IMapper mapper)
        {
            _groupGradeProvider = groupGradeProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                DataClaim.GetClaim(Request);
                var groupGrades = _groupGradeProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = groupGrades.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var groupGrades = _groupGradeProvider.Get(Id);
            if (groupGrades == null)
            {
                return NotFound("Group Grade not found.");
            }

            return Ok(groupGrades);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel groupGrade)
        {
            var model = new GradeGroup();
            if (groupGrade is null)
            {
                return BadRequest("Group Grade is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var groupGradeMapper = _mapper.Map(groupGrade, model);
            _groupGradeProvider.Add(groupGradeMapper);
            return Ok(groupGrade);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Group Grade is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var groupGrade = _groupGradeProvider.Get(Convert.ToInt32(model.ID));
            var groupGradeMapper = _mapper.Map(model, groupGrade);
            _groupGradeProvider.Edit(groupGradeMapper);
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
                foreach (var ID in listID)
                {
                    var ListGrade = _groupGradeProvider.ListGradeDetail(ID).ToList();

                    foreach (var GradeID in ListGrade)
                    {
                        int id = Convert.ToInt32(GradeID.ID);
                        var IsUsed = _groupGradeProvider.IsUsedByEmployee(id);
                        if (IsUsed)
                            return Ok(IsUsed);
                    }
                }
                Array.ForEach(listID, _groupGradeProvider.Delete);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        // VALIDATE api/<controller>/5
        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _groupGradeProvider.validateCombination(model);
            return Ok(isValid);
        }

        [HttpPost]
        [Route("ValidateGrade")]
        public IActionResult ValidateCombinationGrade([FromBody]CreateEditGradeDetailViewModel model)
        {
            //model.GroupGradeID = EncryptionHelper.DecryptUrlParam(model.GroupGradeID);
            //int headerID = Int32.Parse(model.GroupGradeID);
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _groupGradeProvider.validateCombinationGrade(model);
            //var isValid = _groupGradeProvider.validateCombinationGrade(model, headerID);

            return Ok(isValid);
        }

        [HttpPost]
        [Route("ValidateGradeDetail/{code}/{id}")]
        public IActionResult ValidateGradeDetail(string code, string id)
        {
            try
            {
                bool isGroupGradeCodeValid = _groupGradeProvider.IsGradeDetailCodeValid(code, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ValidateGradeDetailName/{name}/{id}")]
        public IActionResult ValidateGradeDetailName(string name, string id)
        {
            try
            {
                bool isGroupGradeCodeValid = _groupGradeProvider.IsGradeDetailNameValid(name, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetIndexGradeDetail/{groupGradeID}")]
        public IActionResult GetIndexGradeDetail(int groupGradeID)
        {
            //int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(groupGradeID));
            var viewModel = new IndexGradeDetailViewModel();
            //var grades = _groupGradeProvider.Get(decryptID);
            var grades = _groupGradeProvider.Get(groupGradeID);

            //viewModel.GroupGradeID = groupGradeID;
            if (grades != null)
            {
                //viewModel.GroupGradeID = groupGradeID.ToString();
                viewModel.GroupCode = grades.Code;
                viewModel.GroupName = grades.Name;
                viewModel.Description = grades.Description;
                //viewModel.ListGrade = GetListGradeDetail(grades,groupGradeID);
            }
            return Ok(viewModel);
        }

        [HttpPost]
        [Route("GetListGradeDetail/{headerID}")]
        public IActionResult GetListGradeDetail([FromBody]CustomDataSourceRequest request, int headerID)
        {
            try
            {
                var grades = _groupGradeProvider.ListGradeDetail(headerID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = grades.ToDataSourceResult(req);

                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("MaxSequenceLevel/{groupGradeID}")]
        public IActionResult MaxSequenceByGroupGradeId(string groupGradeID)
        {
            var model = new CreateEditGradeDetailViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(groupGradeID));
            model.MaxSequence = _groupGradeProvider.MaxSequenceByGroupGradeId(decryptID);
            return Ok(model.MaxSequence);
        }

        [HttpGet]
        [Route("GetGradeDetail/{Id}")]
        public IActionResult GetGradeDetail(int Id)
        {
            var groupGrades = _groupGradeProvider.GetGradeDetail(Id);
            if (groupGrades == null)
            {
                return NotFound("Grade not found.");
            }

            return Ok(groupGrades);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddGradeDetail")]
        public IActionResult AddGradeDetail([FromBody] CreateEditGradeDetailViewModel grade)
        {
            var model = new Grade();
            if (grade is null)
            {
                return BadRequest("Grade is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            grade.GroupGradeID = EncryptionHelper.DecryptUrlParam(grade.GroupGradeID);
            int headerID = Int32.Parse(grade.GroupGradeID);
            //int sequenceLevelBefore = _groupGradeProvider.MaxSequenceByGroupGradeId(headerID) + 1;
            //int sequenceLevelNow = grade.Sequence;
            //_groupGradeProvider.UpdateSequenceLevel(sequenceLevelBefore, sequenceLevelNow,headerID);

            var gradeMapper = _mapper.Map(grade, model);
            _groupGradeProvider.AddGradeDetail(gradeMapper);
            return Ok(grade);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("EditGradeDetail")]
        public IActionResult EditGradeDetail([FromBody] CreateEditGradeDetailViewModel model)
        {
            if (model == null)
            {
                return BadRequest(" Grade is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var grade = _groupGradeProvider.GetGradeDetail(Convert.ToInt32(model.ID));
            //int headerID = grade.GroupGradeId;
            //int sequenceLevelBefore = (int)grade.Sequence;
            //int sequenceLevelNow = model.Sequence;
            //_groupGradeProvider.UpdateSequenceLevel(sequenceLevelBefore, sequenceLevelNow, headerID);

            var gradeMapper = _mapper.Map(model, grade);

            _groupGradeProvider.EditGradeDetail(gradeMapper);


            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("DeleteGradeDetail/{arrayOfID}")]
        public IActionResult DeleteGradeDetail(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                foreach (var ID in listID)
                {
                    var IsUsed = _groupGradeProvider.IsUsedByEmployee(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _groupGradeProvider.DeleteGradeDetail);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

    }
}
