using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chaka.WEB.Models;
using Chaka.ViewModels.SysAdmin;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Chaka.Providers;
using AutoMapper;

namespace Chaka.WEB.Controllers
{
    public class HomeController : Controller
    {

        private IUserProvider _userProvider;
        private IMapper _mapper;

        public HomeController(IUserProvider userProvider, IMapper mapper)
        {
            _userProvider = userProvider;
            _mapper = mapper;
        }

        //private IUserService _userService;
        //private IMapper _mapper;
        //private readonly AppSettings _appSettings;

        //public UsersController(
        //    IUserService userService,
        //    IMapper mapper,
        //    IOptions<AppSettings> appSettings)
        //{
        //    _userService = userService;
        //    _mapper = mapper;
        //    _appSettings = appSettings.Value;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //cobain
        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            //model.EmployeeListFilterLookUp = userProvider.GetEmployeeListFilterLookUp();
            //model.ResponsibilityGroupLookUp = userProvider.GetResponsibilityGroupLookUp();
            return PartialView("CreateEdit", model);
        }

        public IActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var users = _userProvider.List();
            var userDtosViewModel = _mapper.Map<IList<ListUserViewModel>>(users);
            return Ok(userDtosViewModel);
        }

    }
}
