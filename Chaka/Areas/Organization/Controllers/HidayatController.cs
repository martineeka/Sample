using System.Collections.Generic;
using Chaka.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class HidayatController : Controller
    {
        #region FTP Upload 1

        [HttpPost]
        public ActionResult Save(IEnumerable<IFormFile> files)
        {
            if (files != null)
            {
                var Result = FTPHelper.FTPUpload("Image" /*nama folder pada FTP Sesuai Modul*/, files);
                return Ok(Result);
            }
            else
            {
                return NotFound("File Not Found");
            }

        }

        public ActionResult Download(string path, string file)
        {
            var Result = FTPHelper.FTPDownload(path, file);
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".dnct", "application/dotnetcoretutorials");
            string contentType;
            if (!provider.TryGetContentType(file, out contentType))
            {
                contentType = "application/octet-stream";
            }

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Result.FileName,
                Inline = false  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return File(System.IO.File.ReadAllBytes(file), contentType);
        }

        public ActionResult ListFile()
        {
            var list = FTPHelper.listFile();

            return Ok(list);
        }

        public ActionResult Remove(string Path, string File)
        {
            var Result = FTPHelper.FTPRemove(Path + "/" + File);

            return View("Index", Result);
        }
        #endregion

    }
}

