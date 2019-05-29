using Chaka.Providers.SystemAdmin;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.Api.Security.Controllers
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class MainMenuController : ControllerBase
    {
        private readonly IMainMenuProviderService _mainMenuProvider;


        public MainMenuController(IMainMenuProviderService mainMenuProvider)
        {
            _mainMenuProvider = mainMenuProvider;
        }
        [HttpGet]
        [Route("GridMenu")]
        public ActionResult GridMenu()
        {
            var user = _mainMenuProvider.GetUser("fetra.fydhianto");
            var userResponsibilities = _mainMenuProvider.GetUserResponsibilities("fetra.fydhianto");

            return Ok(userResponsibilities);
        }

        [HttpGet]
        [Route("User")]
        public ActionResult Users()
        {
            var user = _mainMenuProvider.GetUser("fetra.fydhianto");
            return Ok(user);
        }

        [HttpGet]
        [Route("Responsibility")]
        public ActionResult Responsibility()
        {
            var userResponsibilities = _mainMenuProvider.GetUserResponsibilities("fetra.fydhianto");
            return Ok(userResponsibilities);
        }


    }
}