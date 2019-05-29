using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chaka.Utilities
{
    public class FTPResult
    {
        public FtpStatusCode Status { get; set; }
        public Stream ReturnValue { get; set; }
        public string CustomReturnValue { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string ContentType { get; set; }
    }
    public static class FTPHelper
    {
        private static readonly string ftpAddress = "ftp://172.16.31.52/";
        private static readonly string ftpUName = "Medion";
        private static readonly string ftpPWord = "Medion2019";

        public static object Log { get; private set; }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static FTPResult FtpCreateFolder(string folderName)
        {
            var _result = new FTPResult();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpAddress + "/" + folderName);
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Credentials = new NetworkCredential(ftpUName, ftpPWord);
                using (var resp = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    _result.Status = resp.StatusCode;
                }

            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                _result.Status = response.StatusCode;
            }

            return _result;
        }

        public static FTPResult FTPUpload(string folderName, IEnumerable<IFormFile> file)
        {
            var _result = new FTPResult();

            foreach (var files in file)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(files.ContentDisposition);
                var fileName = RandomString(6) + "_" + Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + "/" + folderName + "/" + fileName);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUName, ftpPWord);

                byte[] buffer = new byte[500 * 500];
                byte[] arr_ratio;
                Stream strm = files.OpenReadStream();
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = strm.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    arr_ratio = ms.ToArray();
                }
                String b64 = Convert.ToBase64String(arr_ratio);
                string final = "data:" + files.ContentType + ";base64," + b64;
                _result.CustomReturnValue = final;
                _result.ContentType = files.ContentType;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(arr_ratio, 0, arr_ratio.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    _result.Status = response.StatusCode;
                }

                _result.FileName = fileName;
                _result.FolderName = folderName;
            }
            return _result;
        }

        public static FTPResult FTPDownload(string path, string fileName)
        {
            var _result = new FTPResult();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + path + "/" + fileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;

            request.Credentials = new NetworkCredential(ftpUName, ftpPWord);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            using (Stream fileStream = File.Create(fileName))
            {
                responseStream.CopyTo(fileStream);
            }

            reader.Close();
            response.Close();

            using (FtpWebResponse responses = (FtpWebResponse)request.GetResponse())
            {
                _result.Status = responses.StatusCode;
            }

            _result.FileName = fileName;

            return _result;
        }

        public static FTPResult FTPRemove(string path)
        {
            FTPResult _result = new FTPResult();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + path);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftpUName, ftpPWord);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                _result.Status = response.StatusCode;
                response.Close();
            }

            return _result;
        }

        #region Ambil List Dari FTP
        public static List<string> listFile()
        {
            List<string> files = new List<string>();
            FtpWebRequest request = FtpWebRequest.Create(ftpAddress) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(ftpUName, ftpPWord);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            while (!reader.EndOfStream)
            {
                files.Add(reader.ReadLine());
            }

            //Clean-up
            reader.Close();
            responseStream.Close();
            response.Close();

            return files;
        }
        #endregion
    }


}
