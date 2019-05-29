using System;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Chaka.Providers.SystemAdmin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.Menu;
using AutoMapper;
using Chaka.Utilities;
using System.Collections.Generic;
using Chaka.ViewModels;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    [Route("api/Security/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuProvider;
        private IMapper _mapper;


        public MenuController(IMenuService menuProvider,
                              IMapper mapper)
        {
            _menuProvider = menuProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var menus = _menuProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = menus.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("GetListFunction/{menuID}")]
        public IActionResult GetListFunction([FromBody]CustomDataSourceRequest request, int menuID)
        {
            try
            {
                var menus = _menuProvider.GetListMenuFunction(menuID);

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = menus.ToDataSourceResult(req);

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
            var menus = _menuProvider.Get(Id);
            if (menus == null)
            {
                return NotFound("Menu not found.");
            }

            return Ok(menus);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel menu)
        {
            DataClaim.GetClaim(Request);
            if (menu is null)
            {
                return BadRequest("Menu is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var menuMapper = _mapper.Map<Database.Context.Models.Menu>(menu);
            _menuProvider.Add(menuMapper);
            return Ok(menu);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel menu)
        {
            DataClaim.GetClaim(Request);
            if (menu == null)
            {
                return BadRequest("Menu is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var menus = _menuProvider.Get(Convert.ToInt32(menu.ID));
            var menuMapper = _mapper.Map<Database.Context.Models.Menu>(menu);
            menuMapper.Id = menus.Id;
            menuMapper.CreatedDate = menus.CreatedDate;
            menuMapper.CreatedBy = menus.CreatedBy;
            _menuProvider.Edit(menuMapper);
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
                Array.ForEach(listID, _menuProvider.Delete);
            }
            catch (Exception )
            {

                throw;
            }
            return Ok();
        }

        //[HttpGet("Detail/{id}", Name = "Detail")]
        [HttpGet]
        [Route("MenuDetail/{Id}")]
        public IActionResult MenuDetail(int Id)
        {
            var menuDetail = _menuProvider.GetMenuDetailViewModel(Id);
            return Ok(menuDetail);
        }


        // VALIDATE api/<controller>/5
        [HttpPost]
        [Route("ValidateMenuNameEng/{name}/{id}")]
        public IActionResult ValidateMenuNameIna(string name, int id)
        {
            try
            {
                bool isMenuNameValid = _menuProvider.IsMenuNameInaValid(name, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ValidateMenuNameIna/{name}/{id}")]
        public IActionResult ValidateMenuNameEng(string name, int id)
        {
            try
            {
                bool isMenuNameValid = _menuProvider.IsMenuNameEngValid(name, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ListUnselected/{menuID}/")]
        public ActionResult ListUnselected([FromBody] CustomDataSourceRequest request, int menuID)
        {
            try
            {
                int[] selectedIDDes = request.SelectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _menuProvider.ListUnselected(menuID, selectedIDDes);
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
        [Route("ListSelected/{menuID}/")]
        public ActionResult ListSelected([FromBody] CustomDataSourceRequest request, int menuID)
        {
            try
            {
                //convert ke array
                int[] unselectedIDDes = request.UnselectedID.Select(int.Parse).ToArray();
                //int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(request);
                var result = _menuProvider.ListSelected(menuID, unselectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetDetail/{encryptedJobFamilyID}")]
        public IActionResult Index(string encryptedJobFamilyID)
        {
            var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(encryptedJobFamilyID));
            var menu = _menuProvider.Get(decryptID);

            var viewModel = new IndexDetailViewModel();
            viewModel.MenuID = decryptID;
            viewModel.NameENG = menu.NameEng;
            viewModel.NameINA = menu.NameIna;
            
            int[] IDs = null;
            IDs = _menuProvider.GetMenuID(decryptID);

            viewModel.List = _menuProvider.ListUnselected(decryptID, IDs);
            if (viewModel.List == null)
                viewModel.List = new List<ListMenuFunctionViewModel>();

            IDs = viewModel.List.Select(s => s.ID).ToArray();
            viewModel.ListSelected = _menuProvider.ListSelected(decryptID, IDs);
            if (viewModel.ListSelected == null)
                viewModel.ListSelected = new List<ListMenuFunctionViewModel>();

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("UpdateDetail")]
        public ActionResult UpdateDetail([FromBody]IndexDetailViewModel model)
        {
            try
            {
                _menuProvider.UpdateListFunction(model.MenuID, model.Selected);
                return Ok();
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
                return BadRequest(ajaxViewModel);
            }
        }
    }
}