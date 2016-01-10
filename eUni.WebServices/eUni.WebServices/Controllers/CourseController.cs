using AutoMapper;
using eUni.BusinessLogic.Providers;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Dropbox.Api;
using Dropbox.Api.Files;
using eUni.BusinessLogic.IProviders;
using eUni.DataAccess.Enums;
using eUni.WebServices.Helpers;

namespace eUni.WebServices.Controllers
{

    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        private IUserProvider _userProvider;
        private ICourseProvider _courseProvider;
        private IFileProvider _fileProvider;

        public CourseController(IUserProvider userProvider, ICourseProvider courseProvider, IFileProvider fileProvider)
        {
            _userProvider = userProvider;
            _courseProvider = courseProvider;
            _fileProvider = fileProvider;
        }

        [Route("Add")]
        public async Task<IHttpActionResult> Add(CourseViewModel course)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            CourseDTO dtoCourse = Mapper.Map<CourseDTO>(course);
            dtoCourse.Teacher = _userProvider.GetByUserName(Helpers.TokenHelper.GetFromToken(token, "username"));
            _courseProvider.CreateCourse(dtoCourse);

            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"),"Course created"));
            return Content(HttpStatusCode.OK, "Created successfully");
        }

        [Route("AssignTeacher")]
        public async Task<IHttpActionResult> AssignTeacher(string lastName, string firstName, string courseCode)
        {
            CourseDTO course=_courseProvider.GetByCourseCode(courseCode);
            course.Teacher = _userProvider.GetByName(lastName,firstName);
            _courseProvider.UpdateCourse(course);

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(TokenHelper.GetFromToken(token, "username"),"Teacher assigned to course"));
            return Content(HttpStatusCode.OK, "Assigned successfully");
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile(string filename, [FromBody]byte[] contentFile, int courseId, int moduleId)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);

            string username = TokenHelper.GetFromToken(token, "username");
            string role = TokenHelper.GetFromToken(token, "role");

            string path = "/" + role + "/" + username + "/" + filename;

            FileType fileType;
            var lastOrDefault = filename.Split('.').LastOrDefault();
            if (lastOrDefault != null)
                if (Enum.TryParse(lastOrDefault, out fileType))
                {
                    using (var memoryStream = new MemoryStream(contentFile))
                    {
                        FileMetadata uploaded;
                        try
                        {
                            uploaded = await dbx.Files.UploadAsync(
                                path,
                                WriteMode.Add.Instance,
                                body: memoryStream);
                            Logger.Logger.Instance.LogAction(LoggerHelper.GetActionString(username, "Upload File"));
                        }
                        catch (Exception ex)
                        {
                            Logger.Logger.Instance.LogError(ex);
                            return InternalServerError(ex);
                        }

                        if (_fileProvider.SaveUploadedFilePath(new FileDTO
                        {
                            ModuleId = moduleId,Path = path, FileType = fileType,
                            Size = (int) uploaded.Size, Description = filename
                        
                        }))
                            return Ok(path);
                        else
                            return BadRequest("Course or module doesn't exist.");
                    }
                }
                else
                    return BadRequest("File type not supported.");

            return BadRequest("Not a file type.");
        }

        [HttpGet]
        [Route("DownloadLink")]
        public async Task<IHttpActionResult> DownloadLink(int moduleId)
        {
            var key = WebConfigurationManager.AppSettings["DropboxToken"];
            var dbx = new DropboxClient(key);

            var files = _fileProvider.GetFiles(moduleId);

            var model = new List<FileViewModel>();

            foreach (var file in files)
            {
                try
                {
                    var downloadLink = await dbx.Sharing.CreateSharedLinkAsync(file.Path, false);

                    var viewModel = new FileViewModel
                    {
                        Filename = file.Description,
                        Path = downloadLink.Url.Remove(downloadLink.Url.Length - 1) + "1"
                    };
                    model.Add(viewModel);
                }
                catch (Exception ex)
                {
                    Logger.Logger.Instance.LogError(ex);
                    return InternalServerError(ex);
                }
            }
            return Ok(model);
        }
    }
}
