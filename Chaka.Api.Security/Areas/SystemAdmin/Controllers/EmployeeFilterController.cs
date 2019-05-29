using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Chaka.ViewModels.SystemAdmin.EmployeeFilter;
using Chaka.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Security/[controller]")]

    [ApiController]
    public class EmployeeFilterController : ControllerBase
    {
        private readonly IEmployeeFilterService _employeeFilterProvider;
        private IMapper _mapper;
        public EmployeeFilterController(IEmployeeFilterService employeeFilterProvider, IMapper mapper)
        {
            _employeeFilterProvider = employeeFilterProvider;
            _mapper = mapper;
        }

        #region EmployeeFilter
        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var employeeFilter = _employeeFilterProvider.List();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = employeeFilter.ToDataSourceResult(req);
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
            //var filters = _employeeFilterProvider.Get(Id);
            var filters = _employeeFilterProvider.GetEmployeeListFilter(Id);
            if (filters == null)
            {
                return NotFound("Employee Filter not found.");
            }

            return Ok(filters);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel filter)
        {
            if (filter is null)
            {
                return BadRequest("Employee Filter is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var filterMapper = _mapper.Map<Database.Context.Models.EmployeeListFilter>(filter);
            _employeeFilterProvider.Add(filterMapper);
            return Ok(filter);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel empfilter)
        {
            if (empfilter == null)
            {
                return BadRequest("Employee Filter is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var empfilters = _employeeFilterProvider.Get(Convert.ToInt32(empfilter.ID));
            var filterMapper = _mapper.Map<Database.Context.Models.EmployeeListFilter>(empfilter);
            filterMapper.Id = empfilters.Id;
            filterMapper.CreatedDate = empfilters.CreatedDate;
            filterMapper.CreatedBy = empfilters.CreatedBy;
            _employeeFilterProvider.Edit(filterMapper);
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
                Array.ForEach(listID, _employeeFilterProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        // VALIDATE api/<controller>/5
        [HttpPost]
        [Route("Validate/{code}/{id}")]
        public IActionResult Validate(string code, string id)
        {
            try
            {
                bool isMenuNameValid = _employeeFilterProvider.IsCodeValid(code, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        #endregion

        #region u All Filter
        [HttpGet]
        [Route("GetDetail/{Id}")]
        public IActionResult Index(string Id)
        {
            var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(Id));
            var menu = _employeeFilterProvider.GetEmployeeListFilter(decryptID);

            var viewModel = new IndexFilterAllViewModel();
            viewModel.ID = menu.Id;
            viewModel.Code = menu.Code;
            viewModel.Description = menu.Description;
            viewModel.Name = menu.Name;

            //grade
            int[] IDs = null;
            //int[] IDs = { 1 };
            IDs = _employeeFilterProvider.GetFilterGradeID(decryptID);

            viewModel.ListGrade = _employeeFilterProvider.ListGradeUnselected(decryptID, IDs);
            if (viewModel.ListGrade == null)
                viewModel.ListGrade = new List<ListGradeViewModel>();

            IDs = viewModel.ListGrade.Select(s => s.ID).ToArray();

            viewModel.ListGradeSelected = _employeeFilterProvider.ListGradeSelected(decryptID, IDs);
            if (viewModel.ListGradeSelected == null)
                viewModel.ListGradeSelected = new List<ListGradeViewModel>();

            //level
            int[] IDslevel = null;
            IDslevel = _employeeFilterProvider.GetFilterLevelID(decryptID);

            viewModel.ListLevel = _employeeFilterProvider.ListLevelUnselected(decryptID, IDslevel);
            if (viewModel.ListLevel == null)
                viewModel.ListLevel = new List<ListLevelViewModel>();

            IDslevel = viewModel.ListLevel.Select(s => s.ID).ToArray();

            viewModel.ListLevelSelected = _employeeFilterProvider.ListLevelSelected(decryptID, IDslevel);
            if (viewModel.ListLevelSelected == null)
                viewModel.ListLevelSelected = new List<ListLevelViewModel>();

            /////240519  EDi : tambahin JobTitle disini...
            //jobtitle
            int[] IDsjobtitle = null;
            IDsjobtitle = _employeeFilterProvider.GetFilterJobTitleID(decryptID);

            viewModel.ListJobTitle = _employeeFilterProvider.ListJobTitleUnselected(decryptID, IDsjobtitle);
            if (viewModel.ListJobTitle == null)
                viewModel.ListJobTitle = new List<ListJobTitleViewModel>();

            IDsjobtitle = viewModel.ListJobTitle.Select(s => s.ID).ToArray();

            viewModel.ListJobTitleSelected = _employeeFilterProvider.ListJobTitleSelected(decryptID, IDsjobtitle);
            if (viewModel.ListJobTitleSelected == null)
                viewModel.ListJobTitleSelected = new List<ListJobTitleViewModel>();

            //location
            int[] IDslocation = null;
            IDslocation = _employeeFilterProvider.GetFilterLocationID(decryptID);

            viewModel.ListLocation = _employeeFilterProvider.ListLocationUnselected(decryptID, IDslocation);
            if (viewModel.ListLocation == null)
                viewModel.ListLocation = new List<ListLocationViewModel>();

            IDslocation = viewModel.ListLocation.Select(s => s.ID).ToArray();

            viewModel.ListLocationSelected = _employeeFilterProvider.ListLocationSelected(decryptID, IDslocation);
            if (viewModel.ListLocationSelected == null)
                viewModel.ListLocationSelected = new List<ListLocationViewModel>();

            //Organization Unit  edi dikomen dulu ada error context dgn database beda
            //int[] IDsorganizationunit = null;
            //IDsorganizationunit = _employeeFilterProvider.GetFilterOrganizationUnitID(decryptID);

            //viewModel.ListOrganizationUnit = _employeeFilterProvider.ListOrganizationUnitUnselected(decryptID, IDsorganizationunit);
            //if (viewModel.ListOrganizationUnit == null)
            //    viewModel.ListOrganizationUnit = new List<ListOrganizationUnitViewModel>();

            //IDsorganizationunit = viewModel.ListOrganizationUnit.Select(s => s.ID).ToArray();  //edi  ada error di context.. tabel orgunit beda dgn database

            //viewModel.ListOrganizationUnitSelected = _employeeFilterProvider.ListOrganizationUnitSelected(decryptID, IDsorganizationunit);
            //if (viewModel.ListOrganizationUnitSelected == null)
            //    viewModel.ListOrganizationUnitSelected = new List<ListOrganizationUnitViewModel>();

            //employeestatus
            int[] IDsemployeestatus = null;
            IDsemployeestatus = _employeeFilterProvider.GetFilterStatusID(decryptID);

            viewModel.ListEmployeeStatus = _employeeFilterProvider.ListStatusUnselected(decryptID, IDsemployeestatus);
            if (viewModel.ListEmployeeStatus == null)
                viewModel.ListEmployeeStatus = new List<ListEmployeeStatusViewModel>();

            IDsemployeestatus = viewModel.ListEmployeeStatus.Select(s => s.ID).ToArray();

            viewModel.ListEmployeeStatusSelected = _employeeFilterProvider.ListStatusSelected(decryptID, IDsemployeestatus);
            if (viewModel.ListEmployeeStatusSelected == null)
                viewModel.ListEmployeeStatusSelected = new List<ListEmployeeStatusViewModel>();


            return Ok(viewModel);
        }
        #endregion

        #region Filter Grade

        [HttpPost]
        [Route("ListGradeUnselected/{ID}/")]
        public ActionResult ListGradeUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListGradeUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        [Route("ListGradeSelected/{ID}/")]
        public ActionResult ListSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListGradeSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion Filter Grade

        #region Filter Level
        [HttpPost]
        [Route("ListLevelUnselected/{ID}/")]
        public ActionResult ListLevelUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListLevelUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("ListLevelSelected/{ID}/")]
        public ActionResult ListLevelSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListLevelSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Filter Level

        #region Filter Jobtitle
        [HttpPost]
        [Route("ListJobTitleUnselected/{ID}/")]
        public ActionResult ListJobTitleUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListJobTitleUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("ListJobTitleSelected/{ID}/")]
        public ActionResult ListJobTitleSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListJobTitleSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Filter Jobtitle

        #region Filter Location
        [HttpPost]
        [Route("ListLocationUnselected/{ID}/")]
        public ActionResult ListLocationUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListLocationUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("ListLocationSelected/{ID}/")]
        public ActionResult ListLocationSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListLocationSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Filter Location

        #region Filter OrganizationUnit
        [HttpPost]
        [Route("ListOrganizationUnitUnselected/{ID}/")]
        public ActionResult ListOrganizationUnitUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListOrganizationUnitUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("ListOrganizationUnitSelected/{ID}/")]
        public ActionResult ListOrganizationUnitSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListOrganizationUnitSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Filter OrganizationUnit

        #region Filter EmployeeStatus
        [HttpPost]
        [Route("ListEmployeeStatusUnselected/{ID}/")]
        public ActionResult ListEmployeeStatusUnselected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _employeeFilterProvider.ListStatusUnselected(ID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("ListEmployeeStatusSelected/{ID}/")]
        public ActionResult ListEmployeeStatusSelected([FromBody] CustomDataSourceRequest request, int ID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _employeeFilterProvider.ListStatusSelected(ID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Filter EmployeeStatus




        //gak dipake GetDetailGrade/{Id}
        //[HttpGet]
        //[Route("GetDetailGrade/{Id}")]
        //public IActionResult IndexGrade(string Id)
        //{
        //    var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(Id));
        //    var menu = _employeeFilterProvider.GetEmployeeListFilter(decryptID);

        //    var viewModel = new IndexFilterGradeViewModel();
        //    viewModel.EmployeeListFilterID = decryptID;
        //    viewModel.Code = menu.Code;
        //    viewModel.Description = menu.Description;
        //    viewModel.Name = menu.Name;

        //    int[] IDs = null;
        //    //int[] IDs = { 1 };
        //    IDs = _employeeFilterProvider.GetFilterGradeID(decryptID);

        //    viewModel.ListGrade = _employeeFilterProvider.ListGradeUnselected(decryptID, IDs);
        //    if (viewModel.ListGrade == null)
        //        viewModel.ListGrade = new List<ListGradeViewModel>();

        //    IDs = viewModel.ListGrade.Select(s => s.ID).ToArray();

        //    viewModel.ListGradeSelected = _employeeFilterProvider.ListGradeSelected(decryptID, IDs);
        //    if (viewModel.ListGradeSelected == null)
        //        viewModel.ListGradeSelected = new List<ListGradeViewModel>();

        //    return Ok(viewModel);
        //}
        //gak dipake GetFilterGradeIndex
        //[HttpPost]
        //[Route("GetFilterGradeIndex")]
        //public ActionResult FilterGradeIndex(IndexFilterGradeViewModel model)
        //{
        //    try
        //    {
        //        _employeeFilterProvider.UpdateListFilterGrade(model.EmployeeListFilterID, model.GradeSelected);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
        //        return BadRequest(ajaxViewModel);
        //    }
        //}


        //[HttpPost]
        //[Route("GetFilterGrade")]
        //public IActionResult FilterGradeIndex(string encryptedEmployeeListFilterID)
        //{
        //    var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(encryptedEmployeeListFilterID));
        //    var employeeListFilter = _employeeFilterProvider.GetEmployeeListFilter(decryptID);

        //    var viewModel = new IndexFilterGradeViewModel();
        //    viewModel.EmployeeListFilterID = decryptID;
        //    viewModel.Code = employeeListFilter.Code;
        //    viewModel.Name = employeeListFilter.Name;
        //    viewModel.Description = employeeListFilter.Description;

        //    int[] IDs = null;
        //    IDs = _employeeFilterProvider.GetFilterGradeID(decryptID);

        //    viewModel.ListGrade = _employeeFilterProvider.ListGradeUnselected(decryptID, IDs);
        //    if (viewModel.ListGrade == null)
        //        viewModel.ListGrade = new List<ListGradeViewModel>();

        //    IDs = viewModel.ListGrade.Select(s => s.ID).ToArray();
        //    viewModel.ListGradeSelected = _employeeFilterProvider.ListGradeSelected(decryptID, IDs);
        //    if (viewModel.ListGradeSelected == null)
        //        viewModel.ListGradeSelected = new List<ListGradeViewModel>();

        //    return Ok(viewModel);
        //}

        //[HttpGet("Detail/{id}", Name = "Detail")]
        //[HttpGet]
        //[Route("EmpFilterDetail/{Id}")]
        //public IActionResult EmpFilterDetail(int Id)
        //{
        //    var menuDetail = _employeeFilterProvider.GetMenuDetailViewModel(Id);
        //    return Ok(menuDetail);
        //}

        //[HttpGet]
        //[Route("GetDetail/{Id}")]
        //public IActionResult MenuDetail(int Id)
        //{
        //    var menuDetail = _employeeFilterProvider.GetMenuDetail(Id);
        //    return Ok(menuDetail);

        //}

        //[HttpPost]
        //[Route("ListGradeUnselected")]
        //public ActionResult ListGradeUnselected([DataSourceRequest] DataSourceRequest request, int employeeListFilterID, string[] selectedID)
        //{
        //    int[] IDs = null;
        //    if (selectedID == null)
        //        IDs = null;
        //    else
        //        IDs = selectedID.Select(s => Convert.ToInt32(s)).ToArray();
        //    var result = _employeeFilterProvider.ListGradeUnselected(employeeListFilterID, IDs);
        //    return Ok(request);
        //}

        //[HttpPost]
        //[Route("ListGradeSelected")]
        //public ActionResult ListGradeSelected([FromBody] DataSourceRequest request, int employeeListFilterID, string[] unselectedID)
        //{
        //    int[] IDs = null;
        //    if (unselectedID == null)
        //        IDs = null;
        //    else
        //        IDs = unselectedID.Select(s => Convert.ToInt32(s)).ToArray();
        //    var result = _employeeFilterProvider.ListGradeSelected(employeeListFilterID, IDs);
        //    return Ok(request);
        //}

    }
}